using System.Drawing;
using Minesweeper.Constants;

namespace Minesweeper.Skins
{
    public static class BitmapExtension
    {
        public static Bitmap OverlayBitmaps(this Bitmap bottomLayer, Bitmap upperLayer)
        {
            var newBitmap = new Bitmap(GameConstants.CellWidth, GameConstants.CellHeight);
            var rectangle = new Rectangle(0, 0, GameConstants.CellWidth, GameConstants.CellHeight);
            var graph = Graphics.FromImage(newBitmap);
            graph.DrawImage(bottomLayer, rectangle);
            graph.DrawImage(upperLayer, rectangle);
            graph.Dispose();
            return newBitmap;
        }

        public static Bitmap ResizeBitmap(this Bitmap originalBitmap, int newWidth, int newHeight)
        {
            var newBitmap = new Bitmap(newWidth, newHeight);
            var graphics = Graphics.FromImage(newBitmap);
            graphics.DrawImage(originalBitmap, 0, 0, newWidth, newHeight);
            graphics.Dispose();
            return newBitmap;
        }
    }
}