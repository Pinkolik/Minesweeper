using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class FalloutSkin : ISkin
    {
        public Bitmap Field { get; } = Properties.Resources.FalloutField;
        public Bitmap Mine { get; } = Properties.Resources.FalloutMine;
        public Bitmap Tile { get; } = Properties.Resources.FalloutTile;
        public Bitmap Flag { get; } = Properties.Resources.FalloutTile.OverlayBitmaps(Properties.Resources.FalloutFlag);
        public Brush TextBrush { get; } = Brushes.DarkBlue;
    }
}
