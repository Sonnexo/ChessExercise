using ChessExerciseManagement.Models.Moves;

namespace ChessExerciseManagement.Events {
    public class CaptureEventArgs : MoveEventArgs {
        public CaptureEventArgs(CaptureMove captureMove)
            : base(captureMove) {
        }
    }
}
