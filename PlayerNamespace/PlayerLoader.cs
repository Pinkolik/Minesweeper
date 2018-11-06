using System.Collections.Generic;
using Minesweeper.Constants;

namespace Minesweeper.PlayerNamespace
{
    public static class PlayerLoader
    {
        public static HashSet<Player> LoadPlayers()
        {
            HashSet<Player> players;
            try
            {
                players = (HashSet<Player>) Serializer.Serializer.Deserialize(GameConstants.PlayersFileName);
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

        public static void SavePlayer(Player player)
        {
            var players = LoadPlayers();
            players.Remove(player);
            players.Add(player);
            Serializer.Serializer.Serialize(players, GameConstants.PlayersFileName);
        }
    }
}