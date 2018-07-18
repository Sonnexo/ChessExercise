using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Models {
    public class Field : BaseClass {
        public int X {
            get;
        }

        public int Y {
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
