using System.Windows.Forms;
using Minesweeper.Engine;
using Minesweeper.SkillsNamespace;

namespace Minesweeper.UI
{
    public static class MineFieldClickHandler
    {
        public static void HandleFieldClick(this PictureBox pictureBox, MouseEventArgs args, MineField field, Skills skill)
        {
            if (field.GameState == GameState.Paused) return;
            var column = args.X / (pictureBox.Width / field.Columns);
            var row = args.Y / (pictureBox.Height / field.Rows);
            switch (skill)
            {
                case Skills.None:
                    HandleUsualClick(args, field, column, row);
                    break;
                case Skills.SolveSingleCell:
                    field.SolveSingleCell(column, row);
                    break;
                case Skills.SolveNeighbors:
                    field.SolveNeighbors(column, row);
                    break;
            }
        }

        private static void HandleUsualClick(MouseEventArgs args, MineField field, int column, int row)
        {
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