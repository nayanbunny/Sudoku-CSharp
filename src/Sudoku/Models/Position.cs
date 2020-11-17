namespace Sudoku.Models
{
    /// <summary>
    /// Position class to hold Row and Column position of the grid cell.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// The Row Property
        /// </summary>
        /// <returns>Row Position Number</returns>
        public int Row { get; private set; }
        /// <summary>
        /// The Column Property
        /// </summary>
        /// <returns>Column Position Number</returns>
        public int Column { get; private set; }

        /// <summary>
        /// Position Constructor
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}