namespace ChessExerciseManagement.Models.Pieces {
    public class Queen : Piece {
        public Queen() {
            m_key = Affiliation == PlayerAffiliation.Black ? 'q' : 'Q';
        }
    }
}
