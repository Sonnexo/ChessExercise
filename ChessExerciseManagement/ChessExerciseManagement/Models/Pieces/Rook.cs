namespace ChessExerciseManagement.Models.Pieces {
    public class Rook : Piece {
        public Rook() {
            m_key = Affiliation == PlayerAffiliation.Black ? 'r' : 'R';
        }
    }
}
