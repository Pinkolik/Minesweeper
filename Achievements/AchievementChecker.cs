using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minesweeper.Constants;
using Minesweeper.PlayerNamespace;

namespace Minesweeper.Achievements
{
    public static class AchievementChecker
    {
        [AchievementInfo(0, "Just getting started", "Lose 10 times on any field")]
        public static AchievementProgress LoseTenTimes(Player player)
        {
            return new AchievementProgress(player.LostTimes >= 10, true, 10, player.LostTimes);
        }

        [AchievementInfo(1, "First steps", "Win 10 times on any field")]
        public static AchievementProgress WinTenTimes(Player player)
        {
            return new AchievementProgress(player.WonTimes >= 10, true, 10, player.WonTimes);
        }

        [AchievementInfo(2, "Beginner", "Win beginner field for the first time")]
        public static AchievementProgress WinBeginnerField(Player player)
        {
            return new AchievementProgress(!double.IsPositiveInfinity(player.BestTimes[GameConstants.BeginnerSettings]));
        }

        [AchievementInfo(3, "Semi-pro", "Win intermediate field for the first time")]
        public static AchievementProgress WinIntermediateField(Player player)
        {
            return new AchievementProgress(!double.IsPositiveInfinity(player.BestTimes[GameConstants.IntermediateSettings]));
        }

        [AchievementInfo(4, "Almost pro", "Win expert field for the first time")]
        public static AchievementProgress WinExpertField(Player player)
        {
            return new AchievementProgress(!double.IsPositiveInfinity(player.BestTimes[GameConstants.ExpertSettings]));
        }

        [AchievementInfo(5, "Skilled player", "Use at least one skill")]
        public static AchievementProgress UseAtLeastOneSkill(Player player)
        {
            return new AchievementProgress(player.SkillsUsed > 0);
        }

        [AchievementInfo(6, "Fancy", "Buy at least one skin")]
        public static AchievementProgress BuyAtLeastOneSkin(Player player)
        {
            return new AchievementProgress(player.OwnedSkins.Count > 1);
        }

        [AchievementInfo(7, "Cheater", "Use \"XYZZY\" code", true)]
        public static AchievementProgress UseCheatcode(Player player)
        {
            return new AchievementProgress(player.UsedCheatCode);
        }
    }
}
