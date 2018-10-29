using System;
using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper
{
    [Serializable]
    public class MLGSkin : Skin
    {
        public override string SkinName { get; } = "MLG";
        public override Bitmap Field { get; } = Resources.MLGField;
        public override Bitmap Mine { get; } = Resources.MLGMine;
        public override Bitmap Tile { get; } = Resources.MLGTile;
        public override Bitmap Flag { get; } = Resources.MLGTile.OverlayBitmaps(Resources.MLGFlag);
        public override Bitmap DefaultFace { get; } = Resources.MLGDefaultFace;
        public override Bitmap LostFace { get; } = Resources.MLGLostFace;
        public override Bitmap WonFace { get; } = Resources.MLGWonFace;
        public override string WinMessage { get; } = "oh baby a triple";
        public override string LostMessage { get; } = "u got rekt n00b";
        public override Color TextBrushColor { get; } = Color.LimeGreen;
    }
}