using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public static class MineFieldDrawer
    {
        public static void DrawField(this PictureBox pictureBox, MineField mineField)
        {
            var bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            var tempGraphics = Graphics.FromImage(bitmap);
            var myFont = new Font(FontFamily.GenericSerif, 15);
            for (var i = 0; i < mineField.Rows; i++)
            for (var j = 0; j < mineField.Columns; j++)
                DrawMineCell(i, j, tempGraphics, myFont, mineField);

            pictureBox.Image = bitmap;
            myFont.Dispose();
            tempGraphics.Dispose();
        }

        private static void DrawMineCell(int row, int column, Graphics graphics,
            Font font,
            MineField mineField)
        {
            Brush myBrush;
            if (mineField.WasOpened(column, row))
                myBrush = mineField.HasMine(column, row) ? Brushes.Red : Brushes.AntiqueWhite;
            else if (mineField.HasFlag(column, row))
                myBrush = Brushes.Orange;
            else
                myBrush = Brushes.Aqua;
            var rect = new Rectangle(column * GameConstants.CellWidth, row * GameConstants.CellHeight,
                GameConstants.CellWidth, GameConstants.CellHeight);
            graphics.FillRectangle(myBrush, rect);
            graphics.DrawRectangle(Pens.Black, rect);
            if (mineField.WasOpened(column, row)
                && mineField.NeighborMinesCount(column, row) != 0
                && !mineField.HasMine(column, row))
                graphics.DrawString(mineField.NeighborMinesCount(column, row).ToString(),
                    font,
                    Brushes.Black,
                    column * GameConstants.CellWidth + GameConstants.CellWidth / 4,
                    row * GameConstants.CellHeight + GameConstants.CellHeight / 8);
        }
    }
}