using System;

namespace Minesweeper
{
    [Serializable]
    public class FieldSettings
    {
        public FieldSettings(int columns, int rows, int numberOfMines)
        {
            Columns = columns;
            Rows = rows;
            NumberOfMines = numberOfMines;
        }

        public int Columns { get; }
        public int Rows { get; }
        public int NumberOfMines { get; }
    }
}