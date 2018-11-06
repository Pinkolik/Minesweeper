using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper.Skins
{
    [SkinInfo(1, "Shrek", 10000)]
    public class ShrekSkin : ISkin
    {
        public string SkinName { get; } = "Shrek";
        public Bitmap Field { get; } = Resources.ShrekField;
        public Bitmap Mine { get; } = Resources.ShrekMine;
        public Bitmap Tile { get; } = Resources.ShrekTile;
        public Bitmap Flag { get; } = Resources.ShrekTile.OverlayBitmaps(Resources.ShrekFlag);
        public Bitmap DefaultFace { get; } = Resources.ShrekDefaultFace;
        public Bitmap LostFace { get; } = Resources.ShrekLostFace;
        public Bitmap WonFace { get; } = Resources.ShrekWonFace;
        public string WinMessage { get; } = "You saved Fiona";
        public string LostMessage { get; } = "Lord Farquaad killed Fiona";
        public Color TextBrushColor { get; } = Color.Red;
    }
}