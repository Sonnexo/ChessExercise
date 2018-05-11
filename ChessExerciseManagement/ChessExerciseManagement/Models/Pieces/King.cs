namespace ChessExerciseManagement.Models.Pieces {
    public class King : Piece {
        public King() {
            m_key = Affiliation == PlayerAffiliation.Black ? 'k' : 'K';
        }
    }
}
