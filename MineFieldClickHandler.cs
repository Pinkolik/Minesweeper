using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public static class MineFieldClickHandler
    {
        public static void HandleFieldClick(this PictureBox pictureBox, MouseEventArgs args, MineField field)
        {
            var column = args.X / (pictureBox.Width / field.Columns);
            var row = args.Y / (pictureBox.Height / field.Rows);
            switch (args.Button)
            {
                case MouseButtons.Left:
                    field.OpenCell(column, row);
                    break;
                case MouseButtons.Right:
                    field.PutFlag(column, row);
                    break;
                case MouseButtons.Middle:
                    field.TryOpenNeighbors(column, row);
                    break;
            }
        }
    }
}