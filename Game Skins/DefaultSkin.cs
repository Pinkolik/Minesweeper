using System;
using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper
{
    [Serializable]
    public class DefaultSkin : Skin
    {
        public override string SkinName { get; } = "Default";
        public override Bitmap Field { get; } = Resources.DefaultField;
        public override Bitmap Mine { get; } = Resources.DefaultMine;
        public override Bitmap Tile { get; } = Resources.DefaultTile;
        public override Bitmap Flag { get; } = Resources.DefaultTile.OverlayBitmaps(Resources.DefaultFlag);
        public override Bitmap DefaultFace { get; } = Resources.DefaultDefaultFace;
        public override Bitmap LostFace { get; } = Resources.DefaultLostFace;
        public override Bitmap WonFace { get; } = Resources.DefaultWonFace;
        public override string WinMessage { get; } = "Congratulations! You've won!";
        public override string LostMessage { get; } = "You've lost :(";
        public override Color TextBrushColor { get; } = Color.Black;
    }
}