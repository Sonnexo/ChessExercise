﻿using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;

using ChessExerciseManagement.Events;
using ChessExerciseManagement.Base;
using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.UI.UserControls {
    public partial class FieldView : UserControl {
        private FieldController m_field;
        private BoardView m_boardControl;

        private bool m_readonly;

        public FieldView() {
            InitializeComponent();
        }

        public void SetField(FieldController fieldController) {
            m_field = fieldController;
            m_field.PieceChange += Field_PieceChange;

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

            imageViewer.Source = fieldController.PieceController?.GetBitmapImage();
        }

        public void SetBoardControl(BoardView boardControl) {
            m_boardControl = boardControl;
        }

        public void SetReadonly(bool read) {
            m_readonly = read;
        }

        private void Field_PieceChange(object sender, PieceEvent e) {
            if (e.Piece == null) {
                imageViewer.Source = null;
            } else {
                imageViewer.Source = e.Piece.GetBitmapImage();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (m_readonly) {
                return;
            }

            var game = TrainingWindow.Game;
            var markedFieldControls = m_boardControl.MarkedFieldControls;

            if (markedFieldControls.Contains(this)) {
                var markedPiece = m_boardControl.MarkedFieldControl.m_field.Field.Piece;
                markedPiece.SetField(m_field);

                foreach (var fieldControl in markedFieldControls) {
                    fieldControl.BorderBrush = Brushes.Black;
                    fieldControl.BorderThickness = new Thickness(1, 1, 1, 1);
                    m_boardControl.MarkedFieldControl = null;
                }

                markedFieldControls.Clear();
                m_boardControl.MarkedFieldControl = null;

                return;
            }

            foreach (var fieldControl in markedFieldControls) {
                fieldControl.BorderBrush = Brushes.Black;
                fieldControl.BorderThickness = new Thickness(1, 1, 1, 1);
                m_boardControl.MarkedFieldControl = null;
            }

            markedFieldControls.Clear();

            var piece = m_field.Field.Piece;
            if (piece == null || piece.Affiliation != game.WhosTurn) {
                return;
            }

            var fields = piece.GetAccessibleFields();

            foreach (var field in fields) {
                var x = field.X;
                var y = field.Y;

                var control = m_boardControl.Controls[x, y];

                control.BorderBrush = Brushes.Red;
                control.BorderThickness = new Thickness(3.0d);
                markedFieldControls.Add(control);
            }

            m_boardControl.MarkedFieldControl = this;
        }
    }
}
