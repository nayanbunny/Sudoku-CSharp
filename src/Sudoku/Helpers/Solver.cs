using Sudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Helpers
{
    /// <summary>
    /// Solver Class will be used to solve Sudoku Grid using backtracking process.
    /// </summary>
    public class Solver
    {
        /// <summary>
        /// The Grid instance.
        /// </summary>
        readonly Grid grid;
        /// <summary>
        /// Cell indexes that excludes from backtracking.
        /// </summary>
        private readonly List<int> filledCells = new List<int>();
        /// <summary>
        /// Cell Indexes of Invalid Values used while backtracking process.
        /// </summary>
        private List<List<int>> blackListCells;
        /// <summary>
        /// Random object to use creating random numbers.
        /// </summary>
        private readonly Random random = new Random();

        /// <summary>
        /// Solver Constructor
        /// </summary>
        /// <param name="gridInstance">The grid instance.</param>
        public Solver(Grid gridInstance)
        {
            grid = gridInstance ?? new Grid(4, 4);
            InitializeBlackList();
        }

        /// <summary>
        /// Solves the Sudoku Grid.
        /// </summary>
        /// <returns><c>true</c> if the sudoku gird is solved; otherwise, <c>false</c>.</returns>
        public bool Solve()
        {
            // Return false if the current grid is not valid.
            if (!ValidateGrid()) return false;

            // Initialize Filled Cells to preserve which will be used while backtracking.
            IntializeFilledCells();

            // Clear the blacklist
            ClearBlackList();

            int currentCellIndex = 0;

            // Iterate all the cells of the grid.
            while (currentCellIndex < grid.TotalCells)
            {
                // If current cell index already preserved in filled cells, pass it.
                if (filledCells.Contains(currentCellIndex))
                {
                    ++currentCellIndex;
                    continue;
                }

                // Clear blacklists of the indexes after the current index.
                ClearBlackList(cleaningStartIndex: currentCellIndex + 1);

                Cell currentCell = grid.GetCell(cellIndex: currentCellIndex);

                int foundNumber = GetValidNumberForTheCell(currentCellIndex);

                // No valid number found for the cell. Let's backtrack.
                if (foundNumber == 0)
                    currentCellIndex = BackTrackTo(currentCellIndex);
                else
                {
                    // Set found valid value to current cell.
                    grid.SetCellValue(currentCell.Index, foundNumber);
                    ++currentCellIndex;
                }
            }

            return true;
        }

        /// <summary>
        /// Check whether grid is valid.
        /// </summary>
        /// <param name="ignoreEmptyCells">The Ignore Empty Cells.</param>
        /// <returns><c>true</c> if the grid is valid; otherwise, <c>false</c>.</returns>
        public bool ValidateGrid(bool ignoreEmptyCells = false) =>
            grid.Cells.Where(cell => cell.Value != -1)
            .FirstOrDefault(cell => cell.Value != -1 && !IsValidValueForTheCell(cell.Value, cell)) == null;

        /// <summary>
        /// Checks the specified cell value is value for cell.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cell">The cell.</param>
        /// <returns><c>true</c> if the value is valid for the cell; otherwise, <c>false</c>.</returns>
        public bool IsValidValueForTheCell(int value, Cell cell)
        {
            var matchedCell = grid.Cells
                .Where(cellItem => cellItem.Index != cell.Index && (cellItem.GroupNumber == cell.GroupNumber
                || cellItem.Position.Row == cell.Position.Row || cellItem.Position.Column == cell.Position.Column))
                .FirstOrDefault(prop => prop.Value == value);

            return matchedCell == null;
        }

        /// <summary>
        /// Initialize Filled Cells to preserve which will be used while backtracking.
        /// </summary>
        private void IntializeFilledCells()
        {
            filledCells.Clear();
            filledCells.AddRange(grid.Cells.FindAll(cell => cell.Value != -1).Select(cell => cell.Index));
        }

        /// <summary>
        /// Initialize the blacklist.
        /// </summary>
        private void InitializeBlackList()
        {
            blackListCells = new List<List<int>>(grid.TotalCells);
            for (int index = 0; index < blackListCells.Capacity; index++)
                blackListCells.Add(new List<int>());
        }

        /// <summary>
        /// Backtracking operation for the cell specified with index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>BackTrack To Index.</returns>
        private int BackTrackTo(int index)
        {
            // Pass over the protected cells.
            while (filledCells.Contains(--index)) ;

            // Get the back-tracked Cell.
            Cell backTrackedCell = grid.GetCell(index);

            // Add the value to the black-list of the back-tracked cell.
            AddToBlacklist(backTrackedCell.Value, cellIndex: index);

            // Reset the back-tracked cell value.
            backTrackedCell.Value = -1;

            // Reset the blacklist starting from the next one of the current tracking cell.
            ClearBlackList(cleaningStartIndex: index + 1);

            return index;
        }

        /// <summary>
        /// Returns a valid number for the specified cell index.
        /// </summary>
        /// <param name="cellIndex">The cell index.</param>
        /// <returns>Valid Number for the cell index.</returns>
        private int GetValidNumberForTheCell(int cellIndex)
        {
            int foundNumber = 0;
            var possibleNumbers = Enumerable.Range(1, grid.GridSize).ToList();

            // Find valid numbers for the cell.
            var validNumbers = possibleNumbers.Where(val => !blackListCells[cellIndex].Contains(val)).ToArray();

            if (validNumbers.Length > 0)
            {
                // Return a (random) valid number from the valid numbers.
                int choosenIndex = random.Next(validNumbers.Length);
                foundNumber = validNumbers[choosenIndex];
            }

            // Try to get valid (random) value for the current cell, if no any valid value break the loop.
            do
            {
                Cell currentCell = grid.GetCell(cellIndex);

                // Check the found number if valid for the cell, if is not valid number for the cell then add the value to the blacklist of the cell.
                if (foundNumber != 0 && !grid.Solver.IsValidValueForTheCell(foundNumber, currentCell))
                    AddToBlacklist(foundNumber, cellIndex);
                else
                    break;

                // Get a valid (random) value from valid numbers.
                foundNumber = GetValidNumberForTheCell(cellIndex: cellIndex);
            } while (foundNumber != 0);

            return foundNumber;
        }

        /// <summary>
        /// Add value into the specified index of the blacklist. 
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cellIndex">The cell index.</param>
        private void AddToBlacklist(int value, int cellIndex) => blackListCells[cellIndex].Add(value);

        /// <summary>
        /// Clears the backlist after certian cell index.
        /// </summary>
        /// <param name="cleaningStartIndex">Clear the rest of the blacklist starting from the index.</param>
        private void ClearBlackList(int cleaningStartIndex = 0)
        {
            for (int index = cleaningStartIndex; index < blackListCells.Count; index++)
                blackListCells[index].Clear();
        }
    }
}