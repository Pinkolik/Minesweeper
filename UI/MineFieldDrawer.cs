using System.Drawing;
using System.Windows.Forms;
using Minesweeper.Constants;
using Minesweeper.Engine;
using Minesweeper.Skins;

namespace Minesweeper.UI
{
    public static class MineFieldDrawer
    {
        public static void DrawField(this PictureBox pictureBox, MineField mineField, ISkin skin)
        {
            var bitmap = skin.Field.ResizeBitmap(pictureBox.Width, pictureBox.Height);
            if (mineField.GameState != GameState.Paused)
            {
                var tempGraphics = Graphics.FromImage(bitmap);
                var myFont = new Font(FontFamily.GenericSerif, GameConstants.FontSize);
                var fontBrush = new SolidBrush(skin.TextBrushColor);
                for (var i = 0; i < mineField.Rows; i++)
                for (var j = 0; j < mineField.Columns; j++)
                    DrawMineCell(i, j, tempGraphics, myFont, fontBrush, skin, mineField);
                myFont.Dispose();
                fontBrush.Dispose();
                tempGraphics.Dispose();
            }

            pictureBox.Image = bitmap;
        }

        private static void DrawMineCell(int row, int column, Graphics graphics,
            Font font,
            Brush fontBrush,
            ISkin skin,
            MineField mineField)
        {
            Bitmap myImage;
            if (mineField.WasOpened(column, row))
                myImage = mineField.HasMine(column, row) ? skin.Mine : GameConstants.TransparentCell;
            else if (mineField.HasFlag(column, row))
                myImage = skin.Flag;
            else
                myImage = skin.Tile;
            var rect = new Rectangle(column * GameConstants.CellWidth, row * GameConstants.CellHeight,
                GameConstants.CellWidth, GameConstants.CellHeight);

            graphics.DrawImage(myImage, rect);
            graphics.DrawRectangle(Pens.Black, rect);
            if (mineField.WasOpened(column, row)
                && mineField.NeighborMinesCount(column, row) != 0
                && !mineField.HasMine(column, row))
                graphics.DrawString(mineField.NeighborMinesCount(column, row).ToString(),
                    font,
                    fontBrush,
                    column * GameConstants.CellWidth + GameConstants.CellWidth / 4,
                    row * GameConstants.CellHeight + GameConstants.CellHeight / 8);
        }
    }
}