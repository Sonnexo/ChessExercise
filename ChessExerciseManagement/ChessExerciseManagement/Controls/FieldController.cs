using System;
using ChessExerciseManagement.Events;
using ChessExerciseManagement.Models;

namespace ChessExerciseManagement.Controls {
    public class FieldController {
        public Field Field {
            private set;
            get;
        }
        public Action<object, PieceEvent> PieceChange { get; internal set; }

        public PieceController PieceController {
            private set;
            get;
        }

        public FieldController(int x, int y) {
            Field = new Field(x, y);
        }

        public void SetPiece(PieceController pieceController) {
            PieceController = pieceController;
            Field.Piece = pieceController.Piece;
        }
    }
}
