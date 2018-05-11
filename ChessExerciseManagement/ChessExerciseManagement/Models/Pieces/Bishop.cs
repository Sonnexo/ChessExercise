namespace ChessExerciseManagement.Models.Pieces {
    public class Bishop : Piece {
        public Bishop() {
            m_key = Affiliation == PlayerAffiliation.Black ? 'b' : 'B';
        }
    }
}
