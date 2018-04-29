namespace ChessExerciseManagement.Models.Pieces {
    public class Bishop : Piece {
        public Bishop(Player player, Field field) : base(player, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'b' : 'B';
        }
    }
}
