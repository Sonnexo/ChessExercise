using ChessExerciseManagement.Base;
using ChessExerciseManagement.Events;
using ChessExerciseManagement.Models;
using ChessExerciseManagement.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ChessExerciseManagement.Controls {
    public class PieceController {
        public Piece Piece {
            get;
        }

        public PlayerAffiliation PlayerAffiliation {
            get {
                return Piece.Affiliation;
            }
        }

        public event MoveEventHandler Move;
        public delegate void MoveEventHandler(object sender, MoveEvent e);

        public event CaptureEventHandler Capture;
        public delegate void CaptureEventHandler(object sender, CaptureEvent e);

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

        public PieceController() {

        }

        public List<Field> GetAccessibleFields() {
            var fields = new List<Field>();
            switch (Piece.GetFenChar()) {
                case 'k':
                case 'K':
                    break;
                case 'q':
                case 'Q':
                    break;
                case 'r':
                case 'R':
                    break;
                case 'b':
                case 'B':
                    break;
                case 'n':
                case 'N':
                    break;
                case 'p':
                case 'P':
                    break;
            }

            return fields;
        }

        public Bitmap GetBitmap() {
            return PictureHelper.GetPictureHelper(Piece.GetFenChar()).Bitmap;
        }

        public BitmapImage GetBitmapImage() {
            return PictureHelper.GetPictureHelper(Piece.GetFenChar()).BitmapImage;
        }
    }
}
