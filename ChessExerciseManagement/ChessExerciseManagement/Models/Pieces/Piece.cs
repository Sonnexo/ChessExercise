using ChessExerciseManagement.Base;

namespace ChessExerciseManagement.Models.Pieces {
    public abstract class Piece : BaseClass {
        private PlayerAffiliation m_playerAffiliation;
        public PlayerAffiliation PlayerAffiliation {
            set {
                m_playerAffiliation = value;
                if (m_playerAffiliation == PlayerAffiliation.Black) {
                    FenChar = char.ToLower(FenChar);
                }
            }
            get {
                return m_playerAffiliation;
            }
        }

        private Player m_player;
        public Player Player {
            set {
                PlayerAffiliation = value.PlayerAffiliation;
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
            }
            get {
                return m_field;
            }
        }

        public int MoveCounter {
            set;
            get;
        }

        public char FenChar {
            protected set;
            get;
        }
    }
}
