using System.Drawing;

namespace Minesweeper.Skins
{
    public interface ISkin
    {
        string SkinName { get; }
        Bitmap Field { get; }
        Bitmap Mine { get; }
        Bitmap Tile { get; }
        Bitmap Flag { get; }
        Bitmap DefaultFace { get; }
        Bitmap LostFace { get; }
        Bitmap WonFace { get; }
        string WinMessage { get; }
        string LostMessage { get; }
        Color TextBrushColor { get; }
    }
}