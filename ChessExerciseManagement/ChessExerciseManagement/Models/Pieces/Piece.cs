using ChessExerciseManagement.Base;

namespace ChessExerciseManagement.Models.Pieces {
    public abstract class Piece : BaseClass {
        public PlayerAffiliation Affiliation {
            private set;
            get;
        }

        public Player Player {
            private set;
            get;
        }

        public Field Field {
            set;
            get;
        }
        
        public int MoveCounter {
            set;
            get;
        }

        protected char m_key;

        public Piece(Player player, Field field) {
            Player = player;
            Field = field;

            Affiliation = player.PlayerAffiliation;
            field.Piece = this;
        }
        
        public char GetFenChar() {
            return m_key;
        }
    }
}
