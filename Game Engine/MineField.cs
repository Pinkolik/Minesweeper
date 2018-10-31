using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Minesweeper
{
    public class MineField
    {
        #region Game actions help methods

        private void CheckForFirstClick(int column, int row)
        {
            if (GameState != GameState.NotStarted) return;
            FillField(column, row);
            CountNeighbors();
            _gameWatch.Start();
            GameState = GameState.InProgress;
        }

        #endregion

        #region Game state help methods

        private bool TryOpenCell(int column, int row)
        {
            _mineCells[column, row].Open();
            if (!_mineCells[column, row].HasMine) return true;
            GameOver();
            return false;
        }

        #endregion

        #region Constructors

        public MineField(int columns, int rows, int numberOfMines)
        {
            Columns = columns;
            Rows = rows;
            GameState = GameState.NotStarted;
            _mineCells = new MineCell[columns, rows];
            MakeActionWithField((x, y) => _mineCells[x, y] = new MineCell());
            _numberOfMines = numberOfMines;
            _gameWatch = new Stopwatch();
            _cellsToOpen = Columns * Rows - _numberOfMines;
            FieldSettings = new FieldSettings(columns, rows, numberOfMines);
        }

        public MineField(FieldSettings settings) :
            this(settings.Columns, settings.Rows, settings.NumberOfMines)
        {
        }

        #endregion

        #region Private fields

        private readonly int _cellsToOpen;
        private readonly Stopwatch _gameWatch;
        private readonly MineCell[,] _mineCells;
        private readonly int _numberOfMines;
        private int _flagsOnField;

        #endregion

        #region Public members

        public int Columns { get; }
        public int Rows { get; }
        public double TimeElapsed => _gameWatch.ElapsedMilliseconds / 1000.0;
        public int MoneyWon { get; private set; }
        public FieldSettings FieldSettings { get; }
        public GameState GameState { get; private set; }
        public event EventHandler OnGameOver;
        public event EventHandler OnGameWon;

        #endregion

        #region Game actions

        public void TryOpenNeighbors(int column, int row)
        {
            if (!_mineCells[column, row].WasOpened
                || _mineCells[column, row].NeighborMines != _mineCells[column, row].NeighborFlags) return;
            MakeActionWithNeighbors(column, row, OpenCell, (x, y) => !_mineCells[x, y].WasOpened);
        }

        public void PutFlag(int column, int row)
        {
            if (GameState != GameState.InProgress) return;
            if (_mineCells[column, row].WasOpened) return;
            _mineCells[column, row].HasFlag = !_mineCells[column, row].HasFlag;
            MakeActionWithNeighbors(column, row,
                (x, y) => _mineCells[x, y].NeighborFlags += _mineCells[column, row].HasFlag ? 1 : -1);

            _flagsOnField += _mineCells[column, row].HasFlag ? 1 : -1;
        }

        public void OpenCell(int column, int row)
        {
            if (GameState == GameState.Won || GameState == GameState.Lost) return;

            CheckForFirstClick(column, row);

            if (_mineCells[column, row].HasFlag || _mineCells[column, row].WasOpened) return;
            if (!TryOpenCell(column, row)) return;

            _mineCells[column, row].Open();

            if (_mineCells[column, row].NeighborMines != 0) return;
            MakeActionWithNeighbors(column, row, OpenCell, (x, y) => !_mineCells[x, y].WasOpened);
        }

        #endregion

        #region Game state actions

        public void CheckGameState()
        {
            if (GameState != GameState.InProgress || !CheckGameWon()) return;
            ShowMines();
            if (FieldSettings.Equals(GameConstants.BeginnerSettings) ||
                FieldSettings.Equals(GameConstants.IntermediateSettings) ||
                FieldSettings.Equals(GameConstants.ExpertSettings))
                MoneyWon = (int) Math.Round(
                    (_mineCells.Length + _mineCells.Length * 10 * ((double) _numberOfMines / 10) / TimeElapsed) *
                    ((double) _numberOfMines / 10));
            OnGameWon?.Invoke(this, EventArgs.Empty);
        }

        private bool CheckGameWon()
        {
            if (_flagsOnField > _numberOfMines) return false;
            var openCount = _mineCells.Cast<MineCell>().Count(cell => cell.WasOpened);
            if (openCount != _cellsToOpen) return false;
            _gameWatch.Stop();
            GameState = GameState.Won;
            return true;
        }

        private void GameOver()
        {
            _gameWatch.Stop();
            GameState = GameState.Lost;
            ShowMines();
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Game skills

        public void SolveSingleCell(int column, int row)
        {
            if (_mineCells[column, row].HasMine)
            {
                if (!_mineCells[column, row].HasFlag)
                    PutFlag(column, row);
            }
            else
            {
                if (_mineCells[column, row].HasFlag)
                    PutFlag(column, row);
                OpenCell(column, row);
            }
        }

        public void SolveNeighbors(int column, int row)
        {
            SolveSingleCell(column, row);
            MakeActionWithNeighbors(column, row, SolveSingleCell);
        }

        #endregion

        #region Field actions

        public void FillField(int firstColumn, int firstRow)
        {
            var numberOfMines = _numberOfMines;
            var random = new Random();
            while (numberOfMines > 0)
            {
                var column = random.Next(Columns);
                var row = random.Next(Rows);
                if (_mineCells[column, row].HasMine || column == firstColumn && row == firstRow) continue;
                _mineCells[column, row] = new MineCell(true, false, false);
                numberOfMines--;
            }
        }

        private void CountNeighbors()
        {
            List<MineCell> neighborCells;
            MakeActionWithField((column, row) =>
            {
                neighborCells = new List<MineCell>();
                MakeActionWithNeighbors(column, row, (x, y) => neighborCells.Add(_mineCells[x, y]));
                _mineCells[column, row].CountNeighborMines(neighborCells);
            });
        }

        private void ShowMines()
        {
            MakeActionWithField((x, y) =>
            {
                if (_mineCells[x, y].HasMine) _mineCells[x, y].Open();
            });
        }

        #endregion

        #region Field actions help methods

        private void MakeActionWithField(Action<int, int> action)
        {
            for (var i = 0; i < Rows; i++)
            for (var j = 0; j < Columns; j++)
                action(j, i);
        }

        private void MakeActionWithNeighbors(int column, int row, Action<int, int> action)
        {
            MakeActionWithNeighbors(column, row, action, (x, y) => true);
        }

        private void MakeActionWithNeighbors(int column, int row, Action<int, int> action,
            Func<int, int, bool> condition)
        {
            for (var shift = 0; shift < 9; shift++)
            {
                var x = column - 1 + shift % 3;
                var y = row - 1 + shift / 3;
                if (x >= 0 && x < Columns && y >= 0 && y < Rows && !(x == column && y == row) &&
                    condition(x, y))
                    action(x, y);
            }
        }

        #endregion

        #region Drawer help methods

        public bool WasOpened(int column, int row)
        {
            return _mineCells[column, row].WasOpened;
        }

        public bool HasFlag(int column, int row)
        {
            return _mineCells[column, row].HasFlag;
        }

        public bool HasMine(int column, int row)
        {
            return _mineCells[column, row].HasMine;
        }

        public int NeighborMinesCount(int column, int row)
        {
            return _mineCells[column, row].NeighborMines;
        }

        public int FlagsLeft()
        {
            return _numberOfMines - _flagsOnField;
        }

        #endregion
    }
}