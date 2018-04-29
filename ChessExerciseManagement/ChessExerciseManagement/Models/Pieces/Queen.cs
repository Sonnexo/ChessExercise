namespace ChessExerciseManagement.Models.Pieces {
    public class Queen : Piece {
        public Queen(Player player, Field field) : base(player, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'q' : 'Q';
        }
    }
}
