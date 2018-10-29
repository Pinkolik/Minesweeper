using System;
using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper
{
    [Serializable]
    public class ShrekSkin : Skin
    {
        public override string SkinName { get; } = "Shrek";
        public override Bitmap Field { get; } = Resources.ShrekField;
        public override Bitmap Mine { get; } = Resources.ShrekMine;
        public override Bitmap Tile { get; } = Resources.ShrekTile;
        public override Bitmap Flag { get; } = Resources.ShrekTile.OverlayBitmaps(Resources.ShrekFlag);
        public override Bitmap DefaultFace { get; } = Resources.ShrekDefaultFace;
        public override Bitmap LostFace { get; } = Resources.ShrekLostFace;
        public override Bitmap WonFace { get; } = Resources.ShrekWonFace;
        public override string WinMessage { get; } = "You saved Fiona";
        public override string LostMessage { get; } = "Lord Farquaad killed Fiona";
        public override Color TextBrushColor { get; } = Color.Red;
    }
}