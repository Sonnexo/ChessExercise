using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.Generic;

using ChessExerciseManagement.Models;
using ChessExerciseManagement.Controls;
using ChessExerciseManagement.Exercises;
using Microsoft.Win32;
using System.Windows.Controls;

namespace ChessExerciseManagement.UI {
    public partial class ExploreWindow : Window {
        public ExploreWindow() {
            InitializeComponent();
            BoardView.ReadOnly = true;

            var gc = new GameController("64-w");
            BoardView.BoardController = gc.BoardController;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e) {
            Search();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Load();
        }

        private void Search() {
            ExerciseListBox.ItemsSource = new List<string>();

            var text = KeywordTextBox.Text;
            var keywords = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var list = new List<string>();

            for (var i = 0; i < keywords.Length; i++) {
                list.Add(keywords[i].Replace(" ", string.Empty));
            }

            var exercises = ExerciseManager.Filter(list);
            ExerciseListBox.ItemsSource = exercises;
        }

        private void Load() {
            var keys = ExerciseManager.Keys;
            var sb = new StringBuilder();

            foreach (var key in keys) {
                sb.AppendLine(key);
            }

            UsedKeywordTextBox.Text = sb.ToString();
            KeywordTextBox.Text = string.Empty;
            ExerciseListBox.ItemsSource = new List<string>();
            ClearBoard();
        }

        private void ClearBoard() {
            var gc = new GameController("64-w");
            var bc = gc.BoardController;

            BoardView.ReadOnly = true;
            BoardView.BoardController = bc;
        }

        private void UsedkeywordTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            var item = UsedKeywordTextBox.Text;
            var pos = e.GetPosition(UsedKeywordTextBox);

            var lineHeight = pos.Y / 16;

            if (string.IsNullOrEmpty(item)) {
                return;
            }

            var parts = item.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string key;

            try {
                key = parts[(int)lineHeight];
            } catch (Exception) {
                MessageBox.Show("Your click was not perfect, please try again.");
                return;
            }

            key = key + Environment.NewLine;

            if (string.IsNullOrEmpty(KeywordTextBox.Text)) {
                KeywordTextBox.Text = key;
                return;
            }

            if (!KeywordTextBox.Text.Contains(key)) {
                if (!KeywordTextBox.Text.EndsWith("\r\n", StringComparison.InvariantCultureIgnoreCase)) {
                    KeywordTextBox.Text += "\r\n";
                }
                KeywordTextBox.Text += key;
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e) {
            ExerciseManager.Export(StorageManager.Basepath);
        }

        private void ExerciseListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.XButton1 == MouseButtonState.Pressed
                || e.XButton2 == MouseButtonState.Pressed
                || e.MiddleButton == MouseButtonState.Pressed) {
                return;
            }

            if (e.RightButton == MouseButtonState.Pressed && e.LeftButton != MouseButtonState.Pressed) {
                HandleRightClick(sender, e);
                return;
            }

            if (e.RightButton != MouseButtonState.Pressed && e.LeftButton == MouseButtonState.Pressed) {
                HandleLeftClick(sender, e);
                return;
            }
        }

        private void HandleLeftClick(object sender, MouseButtonEventArgs e) {
            var name = (e.OriginalSource as FrameworkElement).DataContext;

            if (name == null) {
                return;
            }

            var fen = File.ReadAllText(name.ToString());

            var gc = new GameController(fen);
            var bc = gc.BoardController;

            BoardView.ReadOnly = true;
            BoardView.BoardController = bc;
        }

        private void HandleRightClick(object sender, MouseButtonEventArgs e) {
            var fe = (e.OriginalSource as FrameworkElement);
            var name = fe.DataContext;

            if (name == null) {
                return;
            }

            var cm = new ContextMenu();

            var editItem = new MenuItem();
            editItem.Header = "Edit";
            editItem.Click += EditItem_Click;

            var deleteItem = new MenuItem();
            deleteItem.Header = "Delete";
            deleteItem.Click += DeleteItem_Click;

            cm.Items.Add(editItem);
            cm.Items.Add(deleteItem);
            fe.ContextMenu = cm;
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e) {
            var mi = sender as MenuItem;
            var fen = mi.DataContext.ToString();

            ExerciseManager.DeleteExercise(fen);
            Search();
            ClearBoard();
        }

        private void EditItem_Click(object sender, RoutedEventArgs e) {
            var mi = sender as MenuItem;
            var fenPath = mi.DataContext.ToString();
            var fen = File.ReadAllText(fenPath);

            var ew = new EditWindow(fen);
            ew.ShowDialog();

            var newFen = ew.ReturnFen;
            if (newFen == null) {
                return;
            }

            Index.EditFile(fenPath, newFen);

            var gc = new GameController(newFen);
            BoardView.BoardController = gc.BoardController;
        }

        private void ExerciseButton_Click(object sender, RoutedEventArgs e) {

            var selectedItems = ExerciseListBox.SelectedItems;
            var numberItems = selectedItems.Count;

            if (numberItems != 4 && numberItems != 6 && numberItems != 9) {
                MessageBox.Show("You must export 4, 6 or 9 images");
                return;
            }

            var annotiationWindow = new AnnotationWindow();
            var dialogResult = annotiationWindow.ShowDialog();

            if (!dialogResult.HasValue || !dialogResult.Value) {
                MessageBox.Show("Could not parse the annotations");
                return;
            }

            var header = annotiationWindow.Header;
            var task = annotiationWindow.Task;
            var captions = annotiationWindow.Captions;

            var exportedImages = new List<Bitmap>();
            foreach (string selectedItem in selectedItems) {
                var selectedFend = File.ReadAllText(selectedItem);
                var gameController = new GameController(selectedFend);
                var boardController = gameController.BoardController;
                exportedImages.Add(boardController.GetImage());
            }

            var filenames = new List<string>();

            foreach (var exportedImage in exportedImages) {
                var filename = StorageManager.GetNewPngPath();
                exportedImage.Save(filename);
                filenames.Add(filename);
            }

            int texfileNumber;

            var texPath = StorageManager.GetNewTexPath(out texfileNumber);
            TexGenerator.GenerateTexFile(texPath, header, task, filenames.ToArray(), captions);

            var args = @"-output-directory " + StorageManager.Outputdir + " ";

            var p = Process.Start("pdflatex.exe", args + texPath);
            p.WaitForExit();

            var p2 = Process.Start(StorageManager.Outputdir + texfileNumber + ".pdf");
            p2.WaitForExit();

        }

        private void ImportButton_Click(object sender, RoutedEventArgs e) {
            var ofd = new OpenFileDialog {
                InitialDirectory = StorageManager.Basepath,
                Filter = "Chess Exercise Exports (*.cee)|*.cee|All files (*.*)|*.*",
                FilterIndex = 1
            };
            var res = ofd.ShowDialog();

            if (res.HasValue && res.Value) {
                ExerciseManager.Import(ofd.FileName);
                Load();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            var result = MessageBox.Show("Are you sure you want to delete the whole database?",
                "Attention", MessageBoxButton.YesNo, MessageBoxImage.Stop, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes) {
                ExerciseManager.Exercises = new Dictionary<string, List<string>>();
                Index.Clear();
                Load();
            }
        }
    }
}
