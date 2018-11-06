using Minesweeper.Constants;

namespace Minesweeper.Engine
{
    public static class SettingsLoader
    {
        public static FieldSettings TryLoadSettings()
        {
            FieldSettings settings;
            try
            {
                settings = (FieldSettings) Serializer.Serializer.Deserialize(GameConstants.SettingsFileName);
            }
            catch
            {
                settings = GameConstants.BeginnerSettings;
            }

            return settings;
        }
    }
}