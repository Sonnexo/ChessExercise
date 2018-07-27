using ChessExerciseManagement.Controls;
using System;

namespace ChessExerciseManagement.Events {
    public class PieceEventArgs : EventArgs {
        public PieceController PieceController {
            get;
        }

        public PieceEventArgs(PieceController piece) {
            PieceController = piece;
        }
    }
}
