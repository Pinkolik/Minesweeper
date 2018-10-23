using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper
{
    public class DefaultSkin : ISkin
    {
        public Bitmap Field { get; } = Resources.DefaultField;
        public Bitmap Mine { get; } = Resources.DefaultMine;
        public Bitmap Tile { get; } = Resources.DefaultTile;
        public Bitmap Flag { get; } = Resources.DefaultTile.OverlayBitmaps(Resources.DefaultFlag);
    }
}