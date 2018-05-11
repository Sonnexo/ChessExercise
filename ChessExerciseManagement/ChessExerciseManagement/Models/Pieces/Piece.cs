using ChessExerciseManagement.Base;

namespace ChessExerciseManagement.Models.Pieces {
    public abstract class Piece : BaseClass {
        public PlayerAffiliation Affiliation {
            set;
            get;
        }

        private Player m_player;
        public Player Player {
            set {
                Affiliation = value.PlayerAffiliation;
                m_player = value;
            }
            get {
                return m_player;
            }
        }

        private Field m_field;
        public Field Field {
            set {
                m_field = value;
                m_field.Piece = this;
            }
            get {
                return m_field;
            }
        }

        public int MoveCounter {
            set;
            get;
        }

        protected char m_key;

        public Piece() {

        }

        public char GetFenChar() {
            return m_key;
        }
    }
}
