using System;
using System.Drawing;
using Minesweeper.Properties;

namespace Minesweeper
{
    [Serializable]
    public class MinecraftSkin : Skin
    {
        public override string SkinName { get; } = "Minecraft";
        public override Bitmap Field { get; } = Resources.MinecraftField;
        public override Bitmap Mine { get; } = Resources.MinecraftMine;
        public override Bitmap Tile { get; } = Resources.MinecraftTile;

        public override Bitmap Flag { get; } =
            Resources.MinecraftTile.OverlayBitmaps(Resources.MinecraftFlag);

        public override Bitmap DefaultFace { get; } = Resources.MinecraftDefaultFace;
        public override Bitmap LostFace { get; } = Resources.MinecraftLostFace;
        public override Bitmap WonFace { get; } = Resources.MinecraftWonFace;
        public override string WinMessage { get; } = "[Mine Hunter] achievement unlocked";
        public override string LostMessage { get; } = "Player was blown up by Creeper";

        public override Color TextBrushColor { get; } = Color.Red;
    }
}