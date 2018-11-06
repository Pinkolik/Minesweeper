using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper.Skins
{
    [SkinInfo(0, "Default", 0)]
    public class DefaultSkin : ISkin
    {
        public string SkinName { get; } = "Default";
        public Bitmap Field { get; } = Resources.DefaultField;
        public Bitmap Mine { get; } = Resources.DefaultMine;
        public Bitmap Tile { get; } = Resources.DefaultTile;
        public Bitmap Flag { get; } = Resources.DefaultTile.OverlayBitmaps(Resources.DefaultFlag);
        public Bitmap DefaultFace { get; } = Resources.DefaultDefaultFace;
        public Bitmap LostFace { get; } = Resources.DefaultLostFace;
        public Bitmap WonFace { get; } = Resources.DefaultWonFace;
        public string WinMessage { get; } = "Congratulations! You've won!";
        public string LostMessage { get; } = "You've lost :(";
        public Color TextBrushColor { get; } = Color.Black;
    }
}