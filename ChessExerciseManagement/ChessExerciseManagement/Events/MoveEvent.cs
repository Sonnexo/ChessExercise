using System;
using ChessExerciseManagement.Models.Moves;

namespace ChessExerciseManagement.Events {
    public class MoveEventArgs : EventArgs {
        public Move Move {
            get;
        }

        public MoveEventArgs(Move move) {
            if (move == null) {
                throw new ArgumentNullException("move must not be null");
            }

            Move = move;
        }
    }
}
