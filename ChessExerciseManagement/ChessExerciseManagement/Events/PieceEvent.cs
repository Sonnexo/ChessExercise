using ChessExerciseManagement.Controls;

namespace ChessExerciseManagement.Events {
    public class PieceEvent {
        public PieceController PieceController {
            get;
        }

        public PieceEvent(PieceController piece) {
            PieceController = piece;
        }
    }
}
