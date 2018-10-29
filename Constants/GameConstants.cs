using System.Collections.Generic;
using System.Drawing;

namespace Minesweeper
{
    public static class GameConstants
    {
        public static string SettingsFileName { get; } = "settings.bin";
        public static string PlayerFileName { get; } = "players.bin";
        public static int CellWidth { get; } = 40;
        public static int CellHeight { get; } = 40;
        public static int FontSize { get; } = 20;
        public static Bitmap TransparentCell { get; } = new Bitmap(CellWidth, CellHeight);
        public static FieldSettings BeginnerSettings { get; } = new FieldSettings(10, 10, 10);
        public static FieldSettings IntermediateSettings { get; } = new FieldSettings(16, 16, 40);
        public static FieldSettings ExpertSettings { get; } = new FieldSettings(30, 16, 99);

        public static Dictionary<Skin, int> Skins { get; } = new Dictionary<Skin, int>
        {
            {new DefaultSkin(), 0},
            {new ShrekSkin(), 10000},
            {new MinecraftSkin(), 50000},
            {new FalloutSkin(), 100000},
            {new MLGSkin(), 999999}
        };
    }
}