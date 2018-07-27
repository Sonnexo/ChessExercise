namespace ChessExerciseManagement.Base {
    public class BaseClass {
        private static int ID;
        private int m_id;

        public BaseClass() {
            m_id = ID++;
        }

        public bool Equals(BaseClass other) {
            if (other == null) {
                return false;
            }

            return other.m_id == m_id;
        }

        public override bool Equals(object obj) {
            var bc = obj as BaseClass;
            if (bc == null) {
                return false;
            }

            return Equals(bc);
        }

        public override int GetHashCode() {
            return m_id;
        }
    }
}
