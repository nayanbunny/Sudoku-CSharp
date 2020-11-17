namespace Sudoku.Models
{
    /// <summary>
    /// Cell to hold details of index, value, group number, postion.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The Index Property
        /// </summary>
        /// <returns>The Index Number</returns>
        public int Index { get; private set; }
        /// <summary>
        /// The Value Property
        /// </summary>
        /// <returns>The Value Number</returns>
        public int Value { get; set; }
        /// <summary>
        /// The Group Number Property
        /// </summary>
        /// <returns>The Group Number</returns>
        public int GroupNumber { get; private set; }
        /// <summary>
        /// The Position Property
        /// </summary>
        /// <returns>The Postion</returns>
        public Position Position { get; private set; }


        /// <summary>
        /// Cell Constructor
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <param name="groupNumber">The Group Number</param>
        /// <param name="position">The Position</param>
        /// <returns>Cell</returns>
        public Cell(int index, int value, int groupNumber, Position position)
        {
            Index = index;
            Value = value;
            GroupNumber = groupNumber;
            Position = position;
        }
    }
}