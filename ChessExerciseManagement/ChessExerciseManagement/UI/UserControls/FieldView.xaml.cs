using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;

using ChessExerciseManagement.Events;
using ChessExerciseManagement.Base;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.UI.UserControls {
    public partial class FieldView : UserControl {
        private FieldController fieldController;
        public FieldController FieldController {
            private get {
                return fieldController;
            }
            set {
                if (fieldController != null) {
                    fieldController.PieceChange -= Field_PieceChange;
                }

                fieldController = value;
                fieldController.PieceChange += Field_PieceChange;

                var field = fieldController.Field;

                if (field.X % 2 != field.Y % 2) {
                    Background = OddBrush;
                } else {
                    Background = EvenBrush;
                }

                if (field.X == 0) {
                    ColumLabel.Content = field.Y + 1;
                    if (field.X % 2 != field.Y % 2) {
                        ColumLabel.Foreground = OddBrush;
                    } else {
                        ColumLabel.Foreground = EvenBrush;
                    }
                }

                if (field.Y == 0) {
                    char a = 'a';
                    RowLabel.Content = a.Load(field.X);
                    if (field.X % 2 != field.Y % 2) {
                        RowLabel.Foreground = OddBrush;
                    } else {
                        RowLabel.Foreground = EvenBrush;
                    }
                }

                ImageViewer.Source = fieldController.PieceController?.GetBitmapImage();
            }
        }

        public BoardView BoardView {
            private get;
            set;
        }

        public bool ReadOnly {
            private get;
            set;
        }

        public Brush OddBrush {
            set;
            get;
        } = Brushes.AliceBlue;

        public Brush EvenBrush {
            set;
            get;
        } = Brushes.RosyBrown;

        public FieldView() {
            InitializeComponent();
        }

        private void Field_PieceChange(object sender, PieceEvent e) {
            if (e.PieceController == null) {
                ImageViewer.Source = null;
            } else {
                ImageViewer.Source = e.PieceController.GetBitmapImage();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (ReadOnly) {
                return;
            }

            var gc = TrainingWindow.GameController;
            var markedFieldControls = BoardView.MarkedFieldControls;

            if (markedFieldControls.Contains(this)) {
                var markedPiece = BoardView.MarkedFieldControl.fieldController.PieceController;
                var oldField = markedPiece.FieldController;

                markedPiece.FieldController = fieldController;
                fieldController.PieceController = markedPiece;

                oldField.PieceController = null;

                foreach (var fieldControl in markedFieldControls) {
                    fieldControl.BorderBrush = Brushes.Black;
                    fieldControl.BorderThickness = new Thickness(1, 1, 1, 1);
                    BoardView.MarkedFieldControl = null;
                }

                markedFieldControls.Clear();
                BoardView.MarkedFieldControl = null;

                gc.Flip();

                return;
            }

            foreach (var fieldControl in markedFieldControls) {
                fieldControl.BorderBrush = Brushes.Black;
                fieldControl.BorderThickness = new Thickness(1, 1, 1, 1);
                BoardView.MarkedFieldControl = null;
            }

            markedFieldControls.Clear();

            var pc = fieldController.PieceController;
            if (pc == null || pc.Piece.PlayerAffiliation != gc.Game.WhosTurn) {
                return;
            }

            var fields = pc.GetAccessibleFields(gc.BoardController);

            foreach (var field in fields) {
                var x = field.X;
                var y = field.Y;

                var fv = BoardView.FieldViews[x, y];

                fv.BorderBrush = Brushes.Red;
                fv.BorderThickness = new Thickness(3.0d);
                markedFieldControls.Add(fv);
            }

            BoardView.MarkedFieldControl = this;
        }
    }
}
