using System.Windows.Controls;
using System.Collections.Generic;

using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.UI.UserControls {
    public partial class BoardView : UserControl {
        public readonly FieldView[,] FieldViews;

        public List<FieldView> MarkedFieldControls;
        public FieldView MarkedFieldControl;

        private bool m_readonly;
        public bool ReadOnly {
            set {
                m_readonly = value;
                foreach (var view in FieldViews) {
                    view.ReadOnly = value;
                }
            }
        }

        private BoardController m_boardController;
        public BoardController BoardController {
            get {
                return m_boardController;
            }
            set {
                m_boardController = value;
                var fields = value.FieldControllers;
                for (int y = 0; y < 8; y++) {
                    for (int x = 0; x < 8; x++) {
                        FieldViews[x, y].FieldController = fields[x, y];
                    }
                }
            }
        }

        public BoardView() {
            InitializeComponent();

            FieldViews = new FieldView[8, 8];
            MarkedFieldControls = new List<FieldView>();

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    var identifier = "f" + x + y;
                    FieldViews[x, y] = (FieldView)FindName(identifier);
                    FieldViews[x, y].BoardView = this;
                }
            }
        }
    }
}
