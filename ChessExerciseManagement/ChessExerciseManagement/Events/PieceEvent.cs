using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Events {
    public class PieceEvent {
        public PieceController PieceController {
            private set;
            get;
        }

        public PieceEvent(PieceController piece) {
            PieceController = piece;
        }
    }
}
