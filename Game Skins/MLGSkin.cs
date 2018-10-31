using System;
using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper
{
    [SkinInfo("MLG", 999999)]
    public class MLGSkin : ISkin
    {
        public string SkinName { get; } = "MLG";
        public Bitmap Field { get; } = Resources.MLGField;
        public Bitmap Mine { get; } = Resources.MLGMine;
        public Bitmap Tile { get; } = Resources.MLGTile;
        public Bitmap Flag { get; } = Resources.MLGTile.OverlayBitmaps(Resources.MLGFlag);
        public Bitmap DefaultFace { get; } = Resources.MLGDefaultFace;
        public Bitmap LostFace { get; } = Resources.MLGLostFace;
        public Bitmap WonFace { get; } = Resources.MLGWonFace;
        public string WinMessage { get; } = "oh baby a triple";
        public string LostMessage { get; } = "u got rekt n00b";
        public Color TextBrushColor { get; } = Color.LimeGreen;
    }
}