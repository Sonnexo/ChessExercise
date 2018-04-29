using System.Windows;
using System.Windows.Controls;

using ChessExerciseManagement.Models;
using System.Collections.Generic;

namespace ChessExerciseManagement.UI.UserControls {
    public partial class BoardView : UserControl {
        private Board m_board;
        public Board Board {
            set {
                m_board = value;

                var fields = value.Fields;
                for (int y = 0; y < 8; y++) {
                    for (int x = 0; x < 8; x++) {
                        FieldViews[x, y].SetField(fields[x, y]);
                    }
                }
            }
            get {
                return m_board;
            }
        }

        public readonly FieldView[,] FieldViews;

        public readonly List<FieldView> MarkedFieldControls;
        public FieldView MarkedFieldControl;

        private bool m_readonly;

        public BoardView() {
            InitializeComponent();

            FieldViews = new FieldView[8, 8];
            MarkedFieldControls = new List<FieldView>();

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    var identifier = "f" + x + y;
                    FieldViews[x, y] = (FieldView)FindName(identifier);
                    FieldViews[x, y].SetBoardControl(this);
                }
            }
        }

        public void SetReadonly(bool read) {
            m_readonly = read;
            foreach (var contr in FieldViews) {
                contr.SetReadonly(read);
            }
        }
    }
}
