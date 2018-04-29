using System.Collections.Generic;
using ChessExerciseManagement.Base;

namespace ChessExerciseManagement.Models {
    public class Board : BaseClass {
        public Field[,] Fields {
            private set;
            get;
        }

        public int Width {
            private set;
            get;
        }

        public int Height {
            private set;
            get;
        }

        public List<Player> Players {
            private set;
            get;
        } = new List<Player>();

        public Board(int width, int height) {
            Fields = new Field[width, height];

            Width = width;
            Height = height;
        }
    }
}
