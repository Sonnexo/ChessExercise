using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using ChessExerciseManagement.Base;
using ChessExerciseManagement.Models;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Controls {
    public partial class PieceController {
        public Piece Piece {
            set;
            get;
        }

        private FieldController m_fieldController;
        public FieldController FieldController {
            get {
                return m_fieldController;
            }
            set {
                m_fieldController = value;
                Piece.Field = m_fieldController.Field;
            }
        }

        public PieceController(Piece piece, PlayerController player, FieldController field) {
            Piece = piece;
            piece.Player = player.Player;

            field.PieceController = this;
            FieldController = field;
        }

        public List<Field> GetAccessibleFields(BoardController bc) {
            var fields = new List<Field>();
            switch (Piece.FenChar) {
                case 'k':
                case 'K':
                    fields.AddRange(MoveHelper.GetAccessibleFieldsKing(bc, Piece));
                    break;
                case 'q':
                case 'Q':
                    fields.AddRange(MoveHelper.GetAccessibleFieldsBishop(bc, Piece));
                    fields.AddRange(MoveHelper.GetAccessibleFieldsRook(bc, Piece));
                    break;
                case 'r':
                case 'R':
                    fields.AddRange(MoveHelper.GetAccessibleFieldsRook(bc, Piece));
                    break;
                case 'b':
                case 'B':
                    fields.AddRange(MoveHelper.GetAccessibleFieldsBishop(bc, Piece));
                    break;
                case 'n':
                case 'N':
                    fields.AddRange(MoveHelper.GetAccessibleFieldsKnight(bc, Piece));
                    break;
                case 'p':
                case 'P':
                    var dY = Piece.PlayerAffiliation == PlayerAffiliation.White ? 1 : -1;
                    fields.AddRange(MoveHelper.GetAccessibleFieldsPawn(bc, Piece, dY));
                    break;
            }

            return fields;
        }

        public Bitmap GetBitmap() => PictureHelper.GetPictureHelper(Piece.FenChar).Bitmap;

        public BitmapImage GetBitmapImage() => PictureHelper.GetPictureHelper(Piece.FenChar).BitmapImage;
    }

    public partial class PieceController {
        static PieceController() {
            var appPath = AppDomain.CurrentDomain.BaseDirectory + @"Images\";

            PictureHelper.AddPicture(appPath + @"RookBlack.png", 'r');
            PictureHelper.AddPicture(appPath + @"RookWhite.png", 'R');
            PictureHelper.AddPicture(appPath + @"KnightBlack.png", 'n');
            PictureHelper.AddPicture(appPath + @"KnightWhite.png", 'N');
            PictureHelper.AddPicture(appPath + @"BishopBlack.png", 'b');
            PictureHelper.AddPicture(appPath + @"BishopWhite.png", 'B');
            PictureHelper.AddPicture(appPath + @"QueenBlack.png", 'q');
            PictureHelper.AddPicture(appPath + @"QueenWhite.png", 'Q');
            PictureHelper.AddPicture(appPath + @"KingBlack.png", 'k');
            PictureHelper.AddPicture(appPath + @"KingWhite.png", 'K');
            PictureHelper.AddPicture(appPath + @"PawnBlack.png", 'p');
            PictureHelper.AddPicture(appPath + @"PawnWhite.png", 'P');
        }
    }
}
