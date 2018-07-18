using ChessExerciseManagement.Controls;
using System;

namespace ChessExerciseManagement.Events {
    public class PieceEvent : EventArgs {
        public PieceController PieceController {
            get;
        }

        public PieceEvent(PieceController piece) {
            PieceController = piece;
        }
    }
}
