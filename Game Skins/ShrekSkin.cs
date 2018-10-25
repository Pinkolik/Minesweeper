using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper
{
    public class ShrekSkin : ISkin
    {
        public Bitmap Field { get; } = Resources.ShrekField;
        public Bitmap Mine { get; } = Resources.ShrekMine;
        public Bitmap Tile { get; } = Resources.ShrekTile;
        public Bitmap Flag { get; } = Resources.ShrekTile.OverlayBitmaps(Resources.ShrekFlag);
        public Brush TextBrush { get; } = Brushes.Red;
    }
}