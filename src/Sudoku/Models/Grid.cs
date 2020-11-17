using Sudoku.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    /// <summary>
    /// Grid of the Sudoku
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// The Total Rows Property
        /// </summary>
        /// <returns>Total Rows Number</returns>
        public int TotalRows { get; private set; }
        /// <summary>
        /// The Total Columns Property
        /// </summary>
        /// <returns>Total Columns Number</returns>
        public int TotalColumns { get; private set; }
        /// <summary>
        /// The Grid Size Property
        /// </summary>
        /// <returns>Grid Size Number</returns>
        public int GridSize { get; private set; }
        /// <summary>
        /// The Sub Grid Size Property
        /// </summary>
        /// <returns>Sub Grid Size Number</returns>
        public int SubGridSize { get; private set; }
        /// <summary>
        /// The Total Cells Property
        /// </summary>
        /// <returns>Total Cells Number</returns>
        public int TotalCells { get => TotalRows * TotalColumns; }
        /// <summary>
        /// The Cells Property
        /// </summary>
        /// <returns>List of Cells</returns>
        public List<Cell> Cells { get; set; }
        /// <summary>
        /// The Solver
        /// </summary>
        /// <returns>The Solver Instance</returns>
        public Solver Solver { get; }

        /// <summary>
        /// Grid Constructor
        /// </summary>
        /// <param name="totalRows">The Total Rows</param>
        /// <param name="totalColumns">The Total Columns</param>
        public Grid(int totalRows, int totalColumns)
        {
            TotalRows = totalRows;
            TotalColumns = totalColumns;
            GridSize = Convert.ToInt16(Math.Sqrt(totalRows * totalColumns));
            SubGridSize = Convert.ToInt16(Math.Sqrt(totalRows));
            Cells = new List<Cell>(TotalCells);
            Solver = new Solver(this);
            InitializeCells();
        }

        /// <summary>
        /// Checks whether the grid is fully filled or not.
        /// </summary>
        /// <returns><c>true</c> if the grid is fully filled otherwise, <c>false</c>.</returns>
        public bool IsGridFilled() => Cells.FirstOrDefault(cell => cell.Value == -1) == null;

        /// <summary>
        /// Check whether grid is empty or not.
        /// </summary>
        /// <returns><c>true</c> if the grid is empty; otherwise, <c>false</c>.</returns>
        public bool IsGridEmpty() => Cells.FirstOrDefault(cell => cell.Value != -1) == null;

        /// <summary>
        /// Reset Cell values to -1. 
        /// </summary>
        public void Clear() => Cells.ForEach(cell => SetCellValue(-1, cell.Index));

        /// <summary>
        /// Get the Cell.
        /// </summary>
        /// <param name="cellIndex">The cell index.</param>
        /// <returns>Cell</returns>
        public Cell GetCell(int cellIndex) => Cells[cellIndex];

        /// <summary>
        /// Set the Cell Value.
        /// </summary>
        /// <param name="cellIndex">The cell index.</param>
        /// <param name="value">The Value.</param>
        public void SetCellValue(int cellIndex, int value) => Cells[cellIndex].Value = value;

        /// <summary>
        /// Initailize the Cells.
        /// </summary>
        private void InitializeCells()
        {
            for (var x = 0; x < TotalRows; x++)
            {
                for (var y = 0; y < TotalColumns; y++)
                {
                    var groupDivider = Convert.ToInt32(Math.Sqrt(TotalRows));
                    Cells.Add(new Cell(
                        index: x * TotalRows + y,
                        value: -1,
                        groupNumber: (x / groupDivider) + groupDivider * (y / groupDivider) + 1,
                        position: new Position(row: x + 1, column: y + 1)
                    ));
                }
            }
        }
    }
}