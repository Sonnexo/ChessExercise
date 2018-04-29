namespace ChessExerciseManagement.Models.Pieces {
    public class Knight : Piece {
        public Knight(Player player, Field field) : base(player, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'n' : 'N';
        }
    }
}
