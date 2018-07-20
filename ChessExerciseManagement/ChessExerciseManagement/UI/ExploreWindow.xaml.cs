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

namespace ChessExerciseManagement.UI {
    public partial class ExploreWindow : Window {
        public ExploreWindow() {
            InitializeComponent();
            BoardView.ReadOnly = true;

            var gc = new GameController("64-w", FenMode.Jonas);
            BoardView.BoardController = gc.BoardController;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e) {
            var text = KeywordTextBox.Text;
            var keywords = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var list = new List<string>();

            for (var i = 0; i < keywords.Length; i++) {
                list.Add(keywords[i].Replace(" ", string.Empty));
            }

            var exercises = ExerciseManager.Filter(list);
            ExerciseListBox.ItemsSource = exercises;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            var keys = ExerciseManager.Keys;
            var sb = new StringBuilder();

            foreach (var key in keys) {
                sb.AppendLine(key);
            }

            UsedKeywordTextBox.Text = sb.ToString();
        }

        private void UsedkeywordTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            var item = UsedKeywordTextBox.Text;
            if (item == null || item == string.Empty) {
                return;
            }

            if (KeywordTextBox.Text == string.Empty) {
                KeywordTextBox.Text = item;
                return;
            }

            if (!KeywordTextBox.Text.Contains(item)) {
                if (!KeywordTextBox.Text.EndsWith("\r\n")) {
                    KeywordTextBox.Text += "\r\n";
                }
                KeywordTextBox.Text += item;
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e) {
            var selectedItems = ExerciseListBox.SelectedItems;
            var numberItems = selectedItems.Count;

            if (numberItems != 4 && numberItems != 6 && numberItems != 9) {
                MessageBox.Show("You must export 4, 6 or 9 images");
                return;
            }

            var annotiationWindow = new AnnotationWindow();
            var dialogResult = annotiationWindow.ShowDialog();

            if(!dialogResult.HasValue || !dialogResult.Value) {
                MessageBox.Show("Could not parse the annotations");
                return;
            }

            var header = annotiationWindow.Header;
            var task = annotiationWindow.Task;
            var captions = annotiationWindow.Captions;

            var exportedImages = new List<Bitmap>();
            foreach (string selectedItem in selectedItems) {
                var selectedFend = File.ReadAllText(selectedItem);
                var gameController = new GameController(selectedFend, FenMode.Jonas);
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

        private void ExerciseListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.XButton1 == MouseButtonState.Pressed
                || e.XButton2 == MouseButtonState.Pressed
                || e.RightButton == MouseButtonState.Pressed
                || e.MiddleButton == MouseButtonState.Pressed
                || e.LeftButton != MouseButtonState.Pressed) {
                return;
            }

            var name = (e.OriginalSource as FrameworkElement).DataContext;

            if (name == null) {
                return;
            }

            var fen = File.ReadAllText(name.ToString());

            var gc = new GameController(fen, FenMode.Jonas);
            var bc = gc.BoardController;

            BoardView.ReadOnly = true;
            BoardView.BoardController = bc;
        }
    }
}
