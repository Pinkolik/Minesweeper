using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class MinecraftSkin : ISkin
    {
        public Bitmap Field { get; } = Properties.Resources.MinecraftField;
        public Bitmap Mine { get; } = Properties.Resources.MinecraftMine;
        public Bitmap Tile { get; } = Properties.Resources.MinecraftTile;

        public Bitmap Flag { get; } =
            Properties.Resources.MinecraftTile.OverlayBitmaps(Properties.Resources.MinecraftFlag);

        public Brush TextBrush { get; } = Brushes.LightGreen;
    }
}
