using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Models {
    public class Field : BaseClass {
        public int X {
            private set;
            get;
        }

        public int Y {
            private set;
            get;
        }

        public Piece Piece {
            set;
            get;
        }

        public Field(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
