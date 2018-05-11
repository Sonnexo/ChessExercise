using System.Collections.Generic;
using ChessExerciseManagement.Models;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Controls {
    public class PlayerController {
        public Player Player {
            private set;
            get;
        }

        public List<PieceController> Pieces {
            private set;
            get;
        } = new List<PieceController>();

        public List<PieceController> LostPieces {
            private set;
            get;
        } = new List<PieceController>();

        public PlayerController(PlayerAffiliation affiliation) {
            Player = new Player(affiliation);
        }

        public bool AddPiece(PieceController newPiece) {
            if (newPiece.PlayerAffiliation != Player.PlayerAffiliation) {
                return false;
            }

            Pieces.Add(newPiece);

            Player.Pieces.Add(newPiece.Piece);
            return true;
        }

        public void NotifyCapturedPiece(PieceController lostPieceController, PieceController capturingPiece) {
            var lostPiece = lostPieceController.Piece;

            Player.Pieces.Remove(lostPiece);
            Player.LostPieces.Add(lostPiece);

            Pieces.Remove(lostPieceController);
            LostPieces.Add(lostPieceController);
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

        public List<Field> GetAccessibleFields(bool onlyAttacked) {
            var list = new List<Field>();

            foreach (var piece in Pieces) {
                if (onlyAttacked && piece.Piece is King) {
                    continue;
                }
                list.AddRange(piece.GetAccessibleFields());
            }

            return list;
        }
    }
}
