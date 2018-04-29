namespace ChessExerciseManagement.Models.Pieces {
    public class Pawn : Piece {
        public Pawn(Player player, Field field) : base(player, field) {
            m_key = Affiliation == PlayerAffiliation.Black ? 'p' : 'P';
        }
    }
}
