﻿using System;
using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Models.Moves {
    public class Move : BaseClass {
        public Field Oldfield {
            get;
        }

        public Field Newfield {
            get;
        }

        public Piece MovedPiece {
            get;
        }

        public bool Check {
            get;
        }

        public bool Mate {
            get;
        }

        public Move(Field oldField, Field newField, Piece movedPiece)
            : this(oldField, newField, movedPiece, false) {

        }

        public Move(Field oldField, Field newField, Piece movedPiece, bool check)
            : this(oldField, newField, movedPiece, check, false) {

        }

        public Move(Field oldField, Field newField, Piece movedPiece, bool check, bool mate) {
            if (oldField == null) {
                throw new ArgumentNullException("oldField must not be null");
            }

            if (newField == null) {
                throw new ArgumentNullException("newField must not be null");
            }

            if (movedPiece == null) {
                throw new ArgumentNullException("movedPiece must not be null");
            }

            if (mate && !check) {
                throw new ArgumentException("There cannot be a mate if it is not check");
            }

            Oldfield = oldField;
            Newfield = newField;
            MovedPiece = movedPiece;

            Check = check;
            Mate = mate;
        }

        public override string ToString() {
            var str = string.Empty;

            var c = char.ToUpper(MovedPiece.FenChar, System.Globalization.CultureInfo.InvariantCulture);
            if (c != 'P') {
                str += c;
            }

            var nX = Newfield.X;
            var nY = Newfield.Y;
            var oX = Oldfield.X;

            str += c.Load(nX);
            str += (nY + 1);

            if (Mate) {
                str += "#";
                return str;
            }

            if (Check) {
                str += "+";
                return str;
            }

            return str;
        }
    }
}
