using System;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Models.Moves {
    public class CastleMove : Move {
        public bool Kingside {
            get;
        }

        public Field OldfieldRook {
            get;
        }

        public Field NewfieldRook {
            get;
        }

        public Piece Rook {
            get;
        }

        public CastleMove(Field oldfieldrook, Field newfieldrook, Piece rook, Field oldfieldking, Field newfieldking, Piece king)
            : this(oldfieldrook, newfieldrook, rook, oldfieldking, newfieldking, king, false) {

        }

        public CastleMove(Field oldfieldrook, Field newfieldrook, Piece rook, Field oldfieldking, Field newfieldking, Piece king, bool check)
            : this(oldfieldrook, newfieldrook, rook, oldfieldking, newfieldking, king, check, false) {

        }

        public CastleMove(Field oldfieldrook, Field newfieldrook, Piece rook, Field oldfieldking, Field newfieldking, Piece king, bool check, bool mate)
            : base(oldfieldking, newfieldking, king, check, mate) {

            if (oldfieldrook == null) {
                throw new ArgumentNullException("oldfieldrook must not be null");
            }

            if (newfieldrook == null) {
                throw new ArgumentNullException("newfieldrook must not be null");
            }

            if (rook == null) {
                throw new ArgumentNullException("rook must not be null");
            }

            var nXR = newfieldrook.X;
            var oXR = oldfieldrook.X;

            Kingside = Math.Abs(nXR - oXR) == 2;

            OldfieldRook = oldfieldrook;
            NewfieldRook = newfieldrook;
            Rook = rook;
        }

        public override string ToString() {
            string str;
            if (Kingside) {
                str = "0-0";
            } else {
                str = "0-0-0";
            }

            if (Mate) {
                return str + "#";
            }

            if (Check) {
                return str + "+";
            }

            return str;
        }
    }
}
