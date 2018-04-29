using System.Collections.Generic;
using ChessExerciseManagement.Models;
using ChessExerciseManagement.Models.Pieces;

namespace ChessExerciseManagement.Controls {
    public class PlayerController {
        public Player Player {
            private set;
            get;
        }

        public PlayerController(PlayerAffiliation affiliation) {
            Player = new Player(affiliation);
        }

        public bool AddPiece(PieceController newPiece) {
            if (newPiece.PlayerAffiliation != Player.PlayerAffiliation) {
                return false;
            }

            Player.Pieces.Add(newPiece.Piece);
            return true;
        }

        public void NotifyCapturedPiece(Piece lostPiece, Piece capturingPiece) {
            Player.Pieces.Remove(lostPiece);
            Player.LostPieces.Add(lostPiece);
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

            foreach (var piece in Player.Pieces) {
                if (onlyAttacked && piece is King) {
                    continue;
                }
                list.AddRange(piece.GetAccessibleFields());
            }

            return list;
        }
    }
}
