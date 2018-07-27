using ChessExerciseManagement.Controls;
using System.Collections.Generic;
using System.Windows;

namespace ChessExerciseManagement.UI {
    public partial class CheckWindow : Window {
        private string[] fensToCheck;
        private int counter;

        public CheckWindow(string[] fens) {
            InitializeComponent();
            fensToCheck = fens;
            counter = 0;
            LoadFen();
        }

        private void LoadFen() {
            var str = fensToCheck[counter];
            FenLabel.Content = str;

            var gc = new GameController(str);
            var bc = gc.BoardController;

            BoardView.ReadOnly = true;
            BoardView.BoardController = bc;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e) {
            counter = (counter - 1 + fensToCheck.Length) % fensToCheck.Length;
            LoadFen();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e) {
            counter = (counter + 1) % fensToCheck.Length;
            LoadFen();
        }
    }
}
