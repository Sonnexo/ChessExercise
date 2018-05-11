using System.Collections.Generic;

using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models.Moves;

namespace ChessExerciseManagement.Models {
    public class Game : BaseClass {
        public PlayerAffiliation WhosTurn {
            set;
            get;
        } = PlayerAffiliation.White;

        public Board Board {
            set;
            get;
        }

        public Player White {
            set;
            get;
        }

        public Player Black {
            set;
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
