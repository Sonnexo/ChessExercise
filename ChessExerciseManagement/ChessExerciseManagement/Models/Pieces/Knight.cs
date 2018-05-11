namespace ChessExerciseManagement.Models.Pieces {
    public class Knight : Piece {
        public Knight() {
            m_key = Affiliation == PlayerAffiliation.Black ? 'n' : 'N';
        }
    }
}
