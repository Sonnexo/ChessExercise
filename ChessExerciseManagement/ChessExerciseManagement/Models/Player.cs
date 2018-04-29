using System.Collections.Generic;
using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Models {
    public class Player : BaseClass {
        public PlayerAffiliation PlayerAffiliation {
            private set;
            get;
        }

        public List<Piece> Pieces {
            private set;
            get;
        } = new List<Piece>();

        public List<Piece> LostPieces {
            private set;
            get;
        } = new List<Piece>();

        public bool MayCastleShort {
            set;
            get;
        } = true;

        public bool MayCastleLong {
            set;
            get;
        } = true;

        public bool InCheck {
            set;
            get;
        } = false;

        public Player(PlayerAffiliation affiliation) {
            PlayerAffiliation = affiliation;
        }
    }
}
