using System;
using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper
{
    [Serializable]
    public class FalloutSkin : Skin
    {
        public override string SkinName { get; } = "Fallout";
        public override Bitmap Field { get; } = Resources.FalloutField;
        public override Bitmap Mine { get; } = Resources.FalloutMine;
        public override Bitmap Tile { get; } = Resources.FalloutTile;
        public override Bitmap Flag { get; } = Resources.FalloutTile.OverlayBitmaps(Resources.FalloutFlag);
        public override Bitmap DefaultFace { get; } = Resources.FalloutDefaultFace;
        public override Bitmap LostFace { get; } = Resources.FalloutLostFace;
        public override Bitmap WonFace { get; } = Resources.FalloutWonFace;
        public override string WinMessage { get; } = "Explosives +10";
        public override string LostMessage { get; } = "Your life has ended in The Wasteland";
        public override Color TextBrushColor { get; } = Color.DarkBlue;
    }
}