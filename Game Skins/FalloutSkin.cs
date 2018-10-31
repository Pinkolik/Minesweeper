using System;
using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper
{
    [SkinInfo("Fallout", 100000)]
    public class FalloutSkin : ISkin
    {
        public string SkinName { get; } = "Fallout";
        public Bitmap Field { get; } = Resources.FalloutField;
        public Bitmap Mine { get; } = Resources.FalloutMine;
        public Bitmap Tile { get; } = Resources.FalloutTile;
        public Bitmap Flag { get; } = Resources.FalloutTile.OverlayBitmaps(Resources.FalloutFlag);
        public Bitmap DefaultFace { get; } = Resources.FalloutDefaultFace;
        public Bitmap LostFace { get; } = Resources.FalloutLostFace;
        public Bitmap WonFace { get; } = Resources.FalloutWonFace;
        public string WinMessage { get; } = "Explosives +10";
        public string LostMessage { get; } = "Your life has ended in The Wasteland";
        public Color TextBrushColor { get; } = Color.DarkBlue;
    }
}