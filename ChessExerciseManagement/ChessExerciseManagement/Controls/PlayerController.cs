using System.Collections.Generic;
using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Controls {
    public class PlayerController {
        public Player Player {
            get;
        }

        public List<PieceController> PieceControllers {
            get;
        } = new List<PieceController>();

        public List<PieceController> LostPieceControllers {
            get;
        } = new List<PieceController>();

        public PlayerController(PlayerAffiliation affiliation) {
            Player = new Player(affiliation);
        }

        public bool AddPiece(PieceController newPiece) {
            if (newPiece.Piece.PlayerAffiliation != Player.PlayerAffiliation) {
                return false;
            }

            PieceControllers.Add(newPiece);

            Player.Pieces.Add(newPiece.Piece);
            return true;
        }

        public void NotifyCapturedPiece(PieceController lostPieceController, PieceController capturingPiece) {
            var lostPiece = lostPieceController.Piece;

            Player.Pieces.Remove(lostPiece);
            Player.LostPieces.Add(lostPiece);

            PieceControllers.Remove(lostPieceController);
            LostPieceControllers.Add(lostPieceController);
        }

        public string GetFenCastle() {
            var mes = string.Empty;

            if (Player.MayCastleLong) {
                mes += Player.PlayerAffiliation == PlayerAffiliation.White ? 'Q' : 'q';
            }
            if (Player.MayCastleShort) {
                mes += Player.PlayerAffiliation == PlayerAffiliation.White ? 'K' : 'k';
            }

            return mes;
        }
    }
}
