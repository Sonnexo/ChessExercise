namespace ChessExerciseManagement.Models.Pieces {
    public class Pawn : Piece {
        public Pawn() {
            m_key = Affiliation == PlayerAffiliation.Black ? 'p' : 'P';
        }
    }
}
