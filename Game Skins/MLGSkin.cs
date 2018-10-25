using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class MLGSkin : ISkin
    {
        public Bitmap Field { get; } = Properties.Resources.MLGField;
        public Bitmap Mine { get; } = Properties.Resources.MLGMine;
        public Bitmap Tile { get; } = Properties.Resources.MLGTile;
        public Bitmap Flag { get; } = Properties.Resources.MLGTile.OverlayBitmaps(Properties.Resources.MLGFlag);
        public Brush TextBrush { get; } = Brushes.LimeGreen;
    }
}
