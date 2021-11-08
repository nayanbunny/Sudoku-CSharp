using Sudoku.Constants;
using Sudoku.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Helpers
{
    /// <summary>
    /// Generator Class will be used to generate Sudoku Grid.
    /// </summary>
    public class Generator
    {
        /// <summary>
        /// The Grid instance.
        /// </summary>
        readonly Grid grid;
        /// <summary>
        /// The Mode.
        /// </summary>
        private readonly string mode;
        /// <summary>
        /// The Solver instance.
        /// </summary>
        private readonly Solver solver;
        /// <summary>
        /// Random object to use creating random numbers.
        /// </summary>
        private readonly Random random = new Random();

        /// <summary>
        /// Generator Constructor
        /// </summary>
        /// <param name="gridInstance">The grid instance.</param>
        public Generator(Grid gridInstance, string gridMode)
        {
            grid = gridInstance ?? new Grid(4, 4);
            mode = gridMode;
            solver = new Solver(grid);
        }

        /// <summary>
        /// Generates the Sudoku Grid.
        /// </summary>
        /// <returns><c>true</c> if the sudoku grid is generated; otherwise, <c>false</c>.</returns>
        public bool Generate()
        {
            solver.Solve();
            GenerateGrid();

            return true;
        }

        /// <summary>
        /// Generate Sudoku Grid with few empty cells.
        /// </summary>
        private void GenerateGrid()
        {
            var cellValueIndexes = (mode, grid.GridSize) switch
            {
                (FormConstants.Hard, 9) => GenerateRandomIndexes(random.Next(16, 24)),
                (FormConstants.Medium, 9) => GenerateRandomIndexes(random.Next(24, 31)),
                (FormConstants.Easy, 9) => GenerateRandomIndexes(random.Next(31, 39)),
                (FormConstants.Hard, 4) => GenerateRandomIndexes(random.Next(1, 4)),
                (FormConstants.Medium, 4) => GenerateRandomIndexes(random.Next(4, 7)),
                _ => GenerateRandomIndexes(random.Next(5, 9))
            };

            grid.Cells.ForEach(cell => cell.Value = !cellValueIndexes.Contains(cell.Index) ? -1 : cell.Value);
        }

        /// <summary>
        /// Generate random indexes which will have cell values.
        /// </summary>
        /// <param name="requiredNumbers"></param>
        /// <returns></returns>
        private List<int> GenerateRandomIndexes(int requiredNumbers)
        {
            return Enumerable.Range(0, requiredNumbers).Select(x => random.Next(0, grid.TotalCells)).ToList();
        }
    }
}