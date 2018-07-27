using System.IO;
using System.Text;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Win32;

using ChessExerciseManagement.Exercises;
using ChessExerciseManagement.Base;

namespace ChessExerciseManagement.UI {
    public partial class FenWindow : Window {
        public FenWindow() {
            InitializeComponent();
        }

        private void CheckFenButton_Click(object sender, RoutedEventArgs e) {
            var input = FenTextBox.Text ?? string.Empty;
            var lines = input.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);

            var listOfIllegalFens = new List<int>();

            for (var i = 0; i < lines.Length; i++) {
                var jonasFenFlag = Fen.CheckJonasFen(lines[i]);

                if (!jonasFenFlag) {
                    listOfIllegalFens.Add(i);
                }
            }

            foreach (var failedNumber in listOfIllegalFens) {
                MessageBox.Show("FEN in line " + (failedNumber + 1) + " could not be parsed.");
            }

            if (listOfIllegalFens.Count == 0 && lines.Length != 0) {
                var checkWindow = new CheckWindow(lines);
                checkWindow.ShowDialog();
            }
        }



        private void SaveFenButton_Click(object sender, RoutedEventArgs e) {
            var saveFileDialog = new SaveFileDialog {
                Filter = "FEN files (*.fen)|*.fen"
            };

            if (saveFileDialog.ShowDialog() == true) {
                var fen = FenTextBox.Text;
                File.WriteAllText(saveFileDialog.FileName, fen);
            }
        }

        private void SaveExerciseButton_Click(object sender, RoutedEventArgs e) {
            var input = FenTextBox.Text ?? string.Empty;
            var lines = input.Split(new[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

            var validLines = new List<string>();
            foreach (var line in lines) {
                if (Fen.CheckJonasFen(line)) {
                    validLines.Add(line);
                }
            }

            var keywordWindow = new KeywordWindow();
            var res = keywordWindow.ShowDialog();

            if (res.HasValue && res.Value) {
                var keywords = keywordWindow.Keywords;
                Index.SaveFens(validLines, keywords);
            }
        }
    }
}
