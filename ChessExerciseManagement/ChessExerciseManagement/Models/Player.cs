using System.Collections.Generic;

using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Models {
    public class Player : BaseClass {
        public PlayerAffiliation PlayerAffiliation {
            get;
        }

        public List<Piece> Pieces {
            get;
        } = new List<Piece>();

        public List<Piece> LostPieces {
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
