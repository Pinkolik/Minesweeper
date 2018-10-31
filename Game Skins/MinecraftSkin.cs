using System;
using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper
{
    [SkinInfo("Minecraft", 50000)]
    public class MinecraftSkin : ISkin
    {
        public string SkinName { get; } = "Minecraft";
        public Bitmap Field { get; } = Resources.MinecraftField;
        public Bitmap Mine { get; } = Resources.MinecraftMine;
        public Bitmap Tile { get; } = Resources.MinecraftTile;

        public Bitmap Flag { get; } =
            Resources.MinecraftTile.OverlayBitmaps(Resources.MinecraftFlag);

        public Bitmap DefaultFace { get; } = Resources.MinecraftDefaultFace;
        public Bitmap LostFace { get; } = Resources.MinecraftLostFace;
        public Bitmap WonFace { get; } = Resources.MinecraftWonFace;
        public string WinMessage { get; } = "[Mine Hunter] achievement unlocked";
        public string LostMessage { get; } = "Player was blown up by Creeper";

        public Color TextBrushColor { get; } = Color.Red;
    }
}