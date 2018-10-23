using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public static class GameConstants
    {
        public static string SettingsFileName { get; } = "settings.bin";
        public static int CellWidth { get; } = 30;
        public static int CellHeight { get; } = 30;
        public static FieldSettings BeginnerSettings { get; } = new FieldSettings(10, 10, 10);
        public static FieldSettings IntermediateSettings { get; } = new FieldSettings(16, 16, 40);
        public static FieldSettings ExpertSettings { get; } = new FieldSettings(30, 16, 99);
    }
}
