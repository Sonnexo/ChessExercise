namespace ChessExerciseManagement.Models.Pieces {
    public class King : Piece {
        public King(Player player, Field field) : base(player, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'k' : 'K';
        }
    }
}