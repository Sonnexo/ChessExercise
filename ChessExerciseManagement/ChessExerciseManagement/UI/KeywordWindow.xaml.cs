using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

using ChessExerciseManagement.Exercises;

namespace ChessExerciseManagement.UI {
    public partial class KeywordWindow : Window {
        public List<string> Keywords {
            get;
        } = new List<string>();

        public KeywordWindow() {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) {
            var text = KeywordTextBox.Text;

            var keywords = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var keyword in keywords) {
                Keywords.Add(keyword.Replace(" ", string.Empty));
            }

            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            var keys = ExerciseManager.Keys;
            var sb = new StringBuilder();

            foreach (var key in keys) {
                sb.AppendLine(key);
            }

            UsedKeywordTextBox.Text = sb.ToString();
        }

        private void UsedkeywordTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
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
    }
}
