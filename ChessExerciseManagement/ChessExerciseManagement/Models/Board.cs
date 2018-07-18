using System.Collections.Generic;

using ChessExerciseManagement.Base;

namespace ChessExerciseManagement.Models {
    public class Board : BaseClass {
        public Field[,] Fields {
            get;
        }

        public int Width {
            get;
        }

        public int Height {
            get;
        }

        public List<Player> Players {
            get;
        } = new List<Player>();

        public Board(int width, int height) {
            Fields = new Field[width, height];

            Width = width;
            Height = height;
        }
    }
}
