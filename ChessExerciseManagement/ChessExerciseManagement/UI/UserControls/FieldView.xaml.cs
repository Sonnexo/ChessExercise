using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;

using ChessExerciseManagement.Events;
using ChessExerciseManagement.Base;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.UI.UserControls {
    public partial class FieldView : UserControl {
        private FieldController m_fieldController;
        private BoardView m_boardView;

        private bool m_readonly;

        public FieldView() {
            InitializeComponent();
        }

        public void SetField(FieldController fieldController) {
            m_fieldController = fieldController;
            m_fieldController.PieceChange += Field_PieceChange;

            var field = fieldController.Field;

            if (field.X % 2 == field.Y % 2) {
                Background = Brushes.AliceBlue;
            } else {
                Background = Brushes.RosyBrown;
            }

            if (field.X == 0) {
                ColumLabel.Content = field.Y + 1;
                if (field.X % 2 != field.Y % 2) {
                    ColumLabel.Foreground = Brushes.AliceBlue;
                } else {
                    ColumLabel.Foreground = Brushes.RosyBrown;
                }
            }

            if (field.Y == 0) {
                char a = 'a';
                RowLabel.Content = a.Load(field.X);
                if (field.X % 2 != field.Y % 2) {
                    RowLabel.Foreground = Brushes.AliceBlue;
                } else {
                    RowLabel.Foreground = Brushes.RosyBrown;
                }
            }

            ImageViewer.Source = fieldController.PieceController?.GetBitmapImage();
        }

        public void SetBoardControl(BoardView boardControl) {
            m_boardView = boardControl;
        }

        public void SetReadonly(bool read) {
            m_readonly = read;
        }

        private void Field_PieceChange(object sender, PieceEvent e) {
            if (e.PieceController == null) {
                ImageViewer.Source = null;
            } else {
                ImageViewer.Source = e.PieceController.GetBitmapImage();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (m_readonly) {
                return;
            }

            var gc = TrainingWindow.GameController;
            var markedFieldControls = m_boardView.MarkedFieldControls;

            if (markedFieldControls.Contains(this)) {
                var markedPiece = m_boardView.MarkedFieldControl.m_fieldController.PieceController;
                markedPiece.SetField(m_fieldController);

                foreach (var fieldControl in markedFieldControls) {
                    fieldControl.BorderBrush = Brushes.Black;
                    fieldControl.BorderThickness = new Thickness(1, 1, 1, 1);
                    m_boardView.MarkedFieldControl = null;
                }

                markedFieldControls.Clear();
                m_boardView.MarkedFieldControl = null;

                return;
            }

            foreach (var fieldControl in markedFieldControls) {
                fieldControl.BorderBrush = Brushes.Black;
                fieldControl.BorderThickness = new Thickness(1, 1, 1, 1);
                m_boardView.MarkedFieldControl = null;
            }

            markedFieldControls.Clear();

            var pc = m_fieldController.PieceController;
            if (pc == null || pc.Piece.Affiliation != gc.Game.WhosTurn) {
                return;
            }

            var fields = pc.GetAccessibleFields();

            foreach (var field in fields) {
                var x = field.X;
                var y = field.Y;

                var fv = m_boardView.FieldViews[x, y];

                fv.BorderBrush = Brushes.Red;
                fv.BorderThickness = new Thickness(3.0d);
                markedFieldControls.Add(fv);
            }

            m_boardView.MarkedFieldControl = this;
        }
    }
}
