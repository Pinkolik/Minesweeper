using System.Collections.Generic;

namespace Minesweeper
{
    public static class PlayerLoader
    {
        public static HashSet<Player> LoadPlayers()
        {
            HashSet<Player> players;
            try
            {
                players = (HashSet<Player>) Serializer.Deserialize(GameConstants.PlayerFileName);
            }
            catch
            {
                players = new HashSet<Player>();
            }

            return players;
        }

        public static Player GetPlayer()
        {
            var selectPlayerForm = new SelectPlayerForm();
            selectPlayerForm.ShowDialog();
            return selectPlayerForm.SelectedPlayer;
        }
    }
}