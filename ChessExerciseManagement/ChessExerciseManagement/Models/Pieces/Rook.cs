namespace ChessExerciseManagement.Models.Pieces {
    public class Rook : Piece {
        public Rook(Player player, Field field) : base(player, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'r' : 'R';
        }
    }
}
