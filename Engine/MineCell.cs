using System.Collections.Generic;

namespace Minesweeper.Engine
{
    public class MineCell
    {
        public MineCell() : this(false, false, false)
        {
        }

        public MineCell(bool hasMine, bool wasOpened, bool hasFlag)
        {
            HasMine = hasMine;
            WasOpened = wasOpened;
            HasFlag = hasFlag;
        }

        public bool HasMine { get; }
        public bool WasOpened { get; private set; }
        public bool HasFlag { get; set; }
        public int NeighborMines { get; private set; }
        public int NeighborFlags { get; set; }

        public void Open()
        {
            WasOpened = true;
        }

        public void CountNeighborMines(IEnumerable<MineCell> neighbors)
        {
            foreach (var cell in neighbors)
                if (cell.HasMine)
                    NeighborMines++;
        }
    }
}