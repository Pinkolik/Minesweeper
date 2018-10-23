namespace Minesweeper
{
    public static class SettingsLoader
    {
        public static FieldSettings TryLoadSettings()
        {
            FieldSettings settings;
            try
            {
                settings = (FieldSettings) Serializer.Deserialize(GameConstants.SettingsFileName);
            }
            catch
            {
                settings = GameConstants.BeginnerSettings;
            }

            return settings;
        }
    }
}