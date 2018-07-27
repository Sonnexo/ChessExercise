using System.Windows;
using System.Windows.Controls;
using ChessExerciseManagement.Base;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.UI {
    public partial class EditWindow : Window {

        public string ReturnFen {
            private set;
            get;
        }

        public EditWindow(string fen) {
            InitializeComponent();
            FenTextBox.Text = fen;

            BoardView.ReadOnly = true;
        }

        private void AbortButton_Click(object sender, RoutedEventArgs e) {
            ReturnFen = null;
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            var fen = FenTextBox.Text;
            if (!Fen.CheckJonasFen(fen)) {
                MessageBox.Show("Cannot save fen if it's not valid.");
                return;
            }

            ReturnFen = fen;
            Close();
        }

        private void FenTextBox_TextChanged(object sender, TextChangedEventArgs e) {
            var fen = FenTextBox.Text;
            if (!Fen.CheckJonasFen(fen)) {
                return;
            }

            var gc = new GameController(fen);
            var bc = gc.BoardController;
            BoardView.BoardController = bc;
        }
    }
}
