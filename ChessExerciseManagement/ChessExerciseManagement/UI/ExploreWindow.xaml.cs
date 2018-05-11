﻿using ChessExerciseManagement.Controls;
using ChessExerciseManagement.Exercises;
using ChessExerciseManagement.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;

namespace ChessExerciseManagement.UI {
    public partial class ExploreWindow : Window {
        public ExploreWindow() {
            InitializeComponent();
            BoardView.SetReadonly(true);

            var gameContorller = new GameController("64-w", FenMode.Jonas);
            var boardController = gameContorller.BoardController;
            BoardView.BoardController = boardController;
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

        private void ExerciseListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (ExerciseListBox.SelectedItem == null) {
                return;
            }

            var viewWindow = new ViewWindow(ExerciseListBox.SelectedItem as string);
            viewWindow.ShowDialog();
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
            var item = UsedKeywordTextBox.SelectedText;
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
            var images = new List<Bitmap>();
            foreach (string item in ExerciseListBox.SelectedItems) {
                var fen = File.ReadAllText(item);
                var gameContorller = new GameController(fen, FenMode.Jonas);
                var boardController = gameContorller.BoardController;
                images.Add(boardController.GetImage());
            }

            var rnd = new Random();
            var filenames = new List<string>();

            foreach (var img in images) {
                var filename = @"C:\Users\fczappa\Desktop\" + rnd.Next() + ".png";
                img.Save(filename);
                filenames.Add(filename);
            }
        }
    }
}
