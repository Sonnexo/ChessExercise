using ChessExerciseManagement.Events;
using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Controls {
    public class FieldController {
        public Field Field {
            get;
        }

        public event PieceEventHandler PieceChange;
        public delegate void PieceEventHandler(object sender, PieceEventArgs e);

        private PieceController m_pieceController;
        public PieceController PieceController {
            set {
                m_pieceController = value;
                Field.Piece = m_pieceController?.Piece;

                PieceChange?.Invoke(this, new PieceEventArgs(m_pieceController));
            }
            get {
                return m_pieceController;
            }
        }

        public FieldController(int x, int y) {
            Field = new Field(x, y);
        }
    }
}
