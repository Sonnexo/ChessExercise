namespace ChessExerciseManagement.Models {
    /// <summary>
    /// Represents the mode of a fen-coded position. Classical is the default value.
    /// </summary>
    public enum FenMode : byte {
        Classical = 0,
        Jonas = 1,
    }
}
