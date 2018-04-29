using System.Collections.Generic;

using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models.Moves;

namespace ChessExerciseManagement.Models {
    public class Game : BaseClass {
        public PlayerAffiliation WhosTurn {
            private set;
            get;
        } = PlayerAffiliation.White;

        public Board Board {
            private set;
            get;
        }

        public Player White {
            private set;
            get;
        }

        public Player Black {
            private set;
            get;
        }

        public int HalfmovesSinceLastCaptureOrPawn {
            set;
            get;
        }

        public int Movecounter {
            set;
            get;
        } = 1;

        public List<Move> Moves {
            private set;
            get;
        } = new List<Move>();

        public Player InCheck {
            set;
            get;
        }

        public Game() {
            Board = new Board(8, 8);
        }
    }
}
