using System.Drawing;

namespace Minesweeper
{
    public interface ISkin
    {
        Bitmap Field { get; }
        Bitmap Mine { get; }
        Bitmap Tile { get; }
        Bitmap Flag { get; }
    }
}