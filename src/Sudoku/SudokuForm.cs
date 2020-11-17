using Sudoku.Constants;
using Sudoku.Helpers;
using Sudoku.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    /// <summary>
    /// Sudoku Form Events Implementation Class.
    /// </summary>
    public partial class SudokuForm : Form
    {
        int ticks = 0;
        bool timerStarted = false;
        bool gridCleared = false;
        int previousGridSize = 4;
        string gridMode = FormConstants.Easy;
        Grid grid;
        readonly List<Label> cellControls = new List<Label>();

        /// <summary>
        /// SudokuForm Constructor
        /// </summary>
        public SudokuForm() => InitializeComponent();

        #region Form Loading Event

        /// <summary>
        /// Sudoku Form Load Event
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event Args</param>
        private void SudokuForm_Load(object sender, EventArgs e)
        {
            cmbBoxMode.SelectedIndex = 0;
            cmbBoxGrid.SelectedIndex = 0;
        }

        #endregion

        #region Timer tick Event

        /// <summary>
        /// Timer Tick Event
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event Args</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            ticks = timerStarted ? ticks + 1 : 0;
            lblTimer.Text = TimeSpan.FromSeconds(ticks).ToString(@"hh\:mm\:ss");

            if (gridCleared)
            {
                gridCleared = false;
                timer.Stop();
            }
        }

        /// <summary>
        /// Message Timer Tick Event
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event Args</param>
        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            ResetStatus();
            messageTimer.Stop();
        }

        #endregion

        #region Click Events

        /// <summary>
        /// Generate Button Click Event
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event Args</param>
        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            timerStarted = false;
            ticks = 0;
            timer.Stop();

            timer.Start();
            timerStarted = true;

            ResetTheGrid();
            var generator = new Generator(grid, gridMode);
            generator.Generate();
            RefreshTheGrid();

            lblStatus.ForeColor = Color.DeepSkyBlue;
            lblStatus.Text = FormConstants.PuzzleGenerated;

            messageTimer.Start();
        }

        /// <summary>
        /// Validate Button Click Event
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event Args</param>
        private void BtnValidate_Click(object sender, EventArgs e)
        {
            string message;
            var messageColor = Color.DodgerBlue;
            
            if (grid.IsGridEmpty())
            {
                messageColor = Color.Orange;
                message = FormConstants.PuzzleGridEmpty;
            }
            else if (grid.IsGridFilled() && grid.Solver.ValidateGrid())
            {
                messageColor = Color.LawnGreen;
                message = FormConstants.CongratulationsMessage;
                timer.Stop();
            }
            else if (grid.IsGridFilled() && !grid.Solver.ValidateGrid())
            {
                messageColor = Color.Red;
                message = FormConstants.PuzzleInvalidSolve;
            }
            else if (!grid.IsGridFilled() && grid.Solver.ValidateGrid(ignoreEmptyCells: true))
                message = FormConstants.PuzzleValidButNotCompleted;
            else
            {
                messageColor = Color.Red;
                message = FormConstants.PuzzleInvalidSolveState;
            }

            lblStatus.ForeColor = messageColor;
            lblStatus.Text = message;

            messageTimer.Interval = 10000;
            messageTimer.Start();
        }

        /// <summary>
        /// Solve Button Click Event
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event Args</param>
        private void BtnSolve_Click(object sender, EventArgs e)
        {
            var solver = new Solver(grid);
            var solved = solver.Solve();

            if (solved)
            {
                RefreshTheGrid();

                lblStatus.ForeColor = Color.LawnGreen;
                lblStatus.Text = FormConstants.PuzzleSolved;

                timer.Stop();
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = FormConstants.PuzzleNoSolution;
            }

            messageTimer.Interval = 7000;
            messageTimer.Start();
        }

        /// <summary>
        /// Reset Button Click Event
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event Args</param>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            timer.Start();
            timerStarted = false;
            gridCleared = true;

            ResetTheGrid();

            lblStatus.ForeColor = Color.White;
            lblStatus.Text = FormConstants.PuzzleCleared;

            messageTimer.Start();
        }

        #endregion

        #region Options Selection Events

        /// <summary>
        /// Mode ComboBox Selection Index Change Event
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event Args</param>
        private void CmbBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridMode = cmbBoxMode.SelectedIndex switch {
                2 => FormConstants.Hard,
                1 => FormConstants.Medium,
                _ => FormConstants.Easy
            };

        }

        /// <summary>
        /// Grid ComboBox Selection Index Change Event
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event Args</param>
        private void CmbBoxGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            int columns;
            int rows = columns = cmbBoxGrid.SelectedIndex == 1 ? 9 : 4;
            grid = new Grid(rows, columns);
            timerStarted = false;
            ResetStatus();
            CreateTheGrid();
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// Creates the Sudoku Grid in Window Form UI
        /// </summary>
        private void CreateTheGrid()
        {
            ClearTheGrid();
            previousGridSize = grid.GridSize + 1;

            var cellTopLocation = 6;
            var cellWidth = grid.GridSize == 9 ? 39 : 91;
            var cellHeight = grid.GridSize == 9 ? 39 : 91;
            var cellFontFamily = FormConstants.FontFamily;
            var cellFontSize = grid.GridSize == 9 ? 16 : 22;

            // Iterates through Rows
            for (var x = 0; x < grid.TotalRows; x++)
            {
                var cellLeftLocation = 5;

                // Iterates through Columns and Place each cell side by side for the current row.
                for (var y = 0; y < grid.TotalColumns; y++)
                {
                    // Control Label within cell
                    var cell = new Label
                    {
                        // Index of the cell
                        Tag = x * grid.TotalRows + y,

                        // UI Properties
                        Width = cellWidth,
                        Height = cellHeight,
                        Left = cellLeftLocation,
                        Top = cellTopLocation,
                        Cursor = Cursors.Hand,
                        ForeColor = Color.Black,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font(cellFontFamily, cellFontSize),
                        BackColor = Color.GhostWhite,
                    };

                    // Click Event for the cell.
                    cell.MouseClick += Cell_Click;
                    // Mouse Hover Event for the cell
                    cell.MouseHover += Cell_Hover;
                    // Mouse Leave Event for the cell
                    cell.MouseLeave += Cell_Leave;

                    // Modify 'cellLeftLocation' with left padding for other cells w.r.t current column.
                    cellLeftLocation += cellWidth + ((y + 1) % grid.SubGridSize == 0 ? 5 : 1);

                    // Add the cell to the 'CellControls' list and to the Grid View.
                    cellControls.Add(cell);
                    gridView.Controls.Add(cell);
                }

                // Modify 'cellTopLocation' with top padding for other cells w.r.t current row.
                cellTopLocation += cellHeight + ((x + 1) % grid.SubGridSize == 0 ? 5 : 2);
            }
        }

        /// <summary>
        /// Hover Event for the cell.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Mouse Event Args</param>
        private void Cell_Hover(object sender, EventArgs e)
        {
            Label cellControl = (sender as Label);
            cellControl.BackColor = Color.ForestGreen;
            cellControl.ForeColor = Color.GhostWhite;
        }

        /// <summary>
        /// Leave Event for the cell.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Mouse Event Args</param>
        private void Cell_Leave(object sender, EventArgs e)
        {
            Label cellControl = (sender as Label);
            cellControl.BackColor = Color.GhostWhite;
            cellControl.ForeColor = Color.Black;
        }

        /// <summary>
        /// Click Event for the cell.
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Mouse Event Args</param>
        private void Cell_Click(object sender, MouseEventArgs e)
        {
            Label clickedCellControl = (sender as Label);
            int cellIndex = (int)clickedCellControl.Tag;

            if (grid.GridSize == 9)
            {
                var numpadGrid9Dialog = new NumpadGrid9Dialog();

                #region Cell Click Location Calcutaion to display Numpad Grid 9 Dialog

                int numpadLocationX = clickedCellControl.Location.X - (numpadGrid9Dialog.Width / 4) + this.Location.X;
                int numpadLocationY = clickedCellControl.Location.Y - (numpadGrid9Dialog.Height / 4) + this.Location.Y;

                if (numpadLocationX < 0) numpadLocationX = 0;
                if (numpadLocationY < 0) numpadLocationY = 0;

                if (Screen.PrimaryScreen.WorkingArea.Width < numpadGrid9Dialog.Width + numpadLocationX)
                    numpadLocationX = Screen.PrimaryScreen.WorkingArea.Width - numpadGrid9Dialog.Width;

                if (Screen.PrimaryScreen.WorkingArea.Height < numpadGrid9Dialog.Height + numpadLocationY)
                    numpadLocationY = Screen.PrimaryScreen.WorkingArea.Height - numpadGrid9Dialog.Height;

                Point numpadLocation = new Point(numpadLocationX, numpadLocationY);
                numpadGrid9Dialog.Location = numpadLocation;

                #endregion

                // Show the numpad dialog.
                numpadGrid9Dialog.Show();

                // Handle the closed event of the numpad dialog to get the result.
                numpadGrid9Dialog.FormClosed += (object s, FormClosedEventArgs ea) =>
                {
                    if (numpadGrid9Dialog.Value != -1 && numpadGrid9Dialog.Value != 0)
                    {
                        grid.SetCellValue(cellIndex, numpadGrid9Dialog.Value);
                        RefreshTheGrid();
                    }
                    else if (numpadGrid9Dialog.Value == 0)
                    {
                        grid.SetCellValue(cellIndex, -1);
                        RefreshTheGrid();
                    }

                    // Dispose the numpad dialog.
                    numpadGrid9Dialog.Dispose();
                };
            }
            else
            {
                var numpadGrid4Dialog = new NumpadGrid4Dialog();

                #region Cell Click Location Calcutaion to display Numpad Grid 9 Dialog

                int numpadLocationX = clickedCellControl.Location.X - (numpadGrid4Dialog.Width / 4) + this.Location.X;
                int numpadLocationY = clickedCellControl.Location.Y - (numpadGrid4Dialog.Height / 4) + this.Location.Y;

                if (numpadLocationX < 0) numpadLocationX = 0;
                if (numpadLocationY < 0) numpadLocationY = 0;

                if (Screen.PrimaryScreen.WorkingArea.Width < numpadGrid4Dialog.Width + numpadLocationX)
                    numpadLocationX = Screen.PrimaryScreen.WorkingArea.Width - numpadGrid4Dialog.Width;

                if (Screen.PrimaryScreen.WorkingArea.Height < numpadGrid4Dialog.Height + numpadLocationY)
                    numpadLocationY = Screen.PrimaryScreen.WorkingArea.Height - numpadGrid4Dialog.Height;

                Point numpadLocation = new Point(numpadLocationX, numpadLocationY);
                numpadGrid4Dialog.Location = numpadLocation;

                #endregion

                // Show the numpad dialog.
                numpadGrid4Dialog.Show();

                // Handle the closed event of the numpad dialog to get the result.
                numpadGrid4Dialog.FormClosed += (object s, FormClosedEventArgs ea) =>
                {
                    if (numpadGrid4Dialog.Value != -1 && numpadGrid4Dialog.Value != 0)
                    {
                        grid.SetCellValue(cellIndex, numpadGrid4Dialog.Value);
                        RefreshTheGrid();
                    }
                    else if (numpadGrid4Dialog.Value == 0)
                    {
                        grid.SetCellValue(cellIndex, -1);
                        RefreshTheGrid();
                    }

                    // Dispose the numpad dialog.
                    numpadGrid4Dialog.Dispose();
                };

            }
        }

        /// <summary>
        /// Reset the Status Label
        /// </summary>
        private void ResetStatus() => lblStatus.Text = string.Empty;

        /// <summary>
        /// Refresh the Sudoku Grid in Window Form UI
        /// </summary>
        private void RefreshTheGrid() 
            => cellControls.ForEach(cell =>
                {
                    var cellIndex = (int)cell.Tag;
                    var cellValue = grid.GetCell(cellIndex).Value;
                    cell.Text = cellValue != -1 ? cellValue.ToString() : string.Empty;
                });

        /// <summary>
        /// Reset the Sudoku Grid in Window Form UI
        /// </summary>
        private void ResetTheGrid()
            => Parallel.Invoke(
                () => cellControls.ForEach(cell => cell.Text = string.Empty),
                () => grid.Cells.ForEach(prop => prop.Value = -1));

        /// <summary>
        /// Clear the Sudoku Grid
        /// </summary>
        private void ClearTheGrid()
        {
            cellControls.Clear();
            for (int i = 0; i < previousGridSize; i++) gridView.Controls.Clear();
            gridView.BackgroundColor = Color.FromArgb(47, 47, 47);
            gridView.Refresh();
        }

        #endregion
    }
}