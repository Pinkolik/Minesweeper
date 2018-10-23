using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public static class MineFieldDrawer
    {
        public static void DrawField(this PictureBox pictureBox, MineField mineField, ISkin skin)
        {
            var bitmap = skin.Field.ResizeBitmap(pictureBox.Width, pictureBox.Height);
            var tempGraphics = Graphics.FromImage(bitmap);
            var myFont = new Font(FontFamily.GenericSerif, 15);
            for (var i = 0; i < mineField.Rows; i++)
            for (var j = 0; j < mineField.Columns; j++)
                DrawMineCell(i, j, tempGraphics, myFont, skin, mineField);

            pictureBox.Image = bitmap;
            myFont.Dispose();
            tempGraphics.Dispose();
        }

        private static void DrawMineCell(int row, int column, Graphics graphics,
            Font font,
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
                    Brushes.White,
                    column * GameConstants.CellWidth + GameConstants.CellWidth / 4,
                    row * GameConstants.CellHeight + GameConstants.CellHeight / 8);
        }
    }
}