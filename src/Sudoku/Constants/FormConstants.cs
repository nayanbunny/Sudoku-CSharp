namespace Sudoku.Constants
{
    /// <summary>
    /// Constants used across the application files.
    /// </summary>
    public class FormConstants
    {
        // Grid Modes
        public const string Easy = "Easy";
        public const string Medium = "Medium";
        public const string Hard = "Hard";

        // Messages
        public const string PuzzleGenerated = "Sudoku puzzle generated!";
        public const string PuzzleSolved = "Sudoku puzzle solved!";
        public const string PuzzleCleared = "Sudoku puzzle grid cleared.";
        public const string CongratulationsMessage = "Congratulations!, the sudoku puzzle is solved.";
        public const string PuzzleGridEmpty = "The Sudoku puzzle grid is empty.";
        public const string PuzzleInvalidSolve = "Sorry, the sudoku puzzle is not solved correctly.";
        public const string PuzzleValidButNotCompleted = "The current state of the sudoku puzzle is correct, but not completed yet.";
        public const string PuzzleInvalidSolveState = "Sorry, the current state of the sudoku puzzle is incorrect.";
        public const string PuzzleNoSolution = "No solution for this sudoku puzzle.";

        //Fonts
        public const string FontFamily = "Maiandra GD";
    }
}