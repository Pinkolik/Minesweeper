using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Minesweeper.Constants;
using Minesweeper.PlayerNamespace;

namespace Minesweeper.Achievements
{
    public static class AchievementChecker
    {
        private static readonly Dictionary<MethodInfo, AchievementInfo> _achievementsDictionary;

        static AchievementChecker()
        {
            _achievementsDictionary = typeof(AchievementChecker)
                .GetMethods(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Where(methodInfo => !methodInfo.Name.StartsWith("get_"))
                .ToDictionary(methodInfo => methodInfo,
                    methodInfo => (AchievementInfo) methodInfo.GetCustomAttribute(typeof(AchievementInfo)));
            AchievementDictionary = new ReadOnlyDictionary<MethodInfo, AchievementInfo>(_achievementsDictionary);
        }

        public static ReadOnlyDictionary<MethodInfo, AchievementInfo> AchievementDictionary { get; }

        [AchievementInfo("Just getting started", "Lose 10 times on any field")]
        public static AchievementProgress LoseTenTimes(Player player)
        {
            return new AchievementProgress(player.LostTimes >= 10, true, 10, player.LostTimes);
        }

        [AchievementInfo("First steps", "Win 10 times on any field")]
        public static AchievementProgress WinTenTimes(Player player)
        {
            return new AchievementProgress(player.WonTimes >= 10, true, 10, player.WonTimes);
        }

        [AchievementInfo("Beginner", "Win beginner field for the first time")]
        public static AchievementProgress WinBeginnerField(Player player)
        {
            return new AchievementProgress(
                !double.IsPositiveInfinity(player.BestTimes[GameConstants.BeginnerSettings]));
        }

        [AchievementInfo("Semi-pro", "Win intermediate field for the first time")]
        public static AchievementProgress WinIntermediateField(Player player)
        {
            return new AchievementProgress(
                !double.IsPositiveInfinity(player.BestTimes[GameConstants.IntermediateSettings]));
        }

        [AchievementInfo("Almost pro", "Win expert field for the first time")]
        public static AchievementProgress WinExpertField(Player player)
        {
            return new AchievementProgress(!double.IsPositiveInfinity(player.BestTimes[GameConstants.ExpertSettings]));
        }

        [AchievementInfo("Skilled player", "Use at least one skill")]
        public static AchievementProgress UseAtLeastOneSkill(Player player)
        {
            return new AchievementProgress(player.SkillsUsed > 0);
        }

        [AchievementInfo("Fancy", "Buy at least one skin")]
        public static AchievementProgress BuyAtLeastOneSkin(Player player)
        {
            return new AchievementProgress(player.OwnedSkins.Count > 1);
        }

        [AchievementInfo("Cheater", "Use \"XYZZY\" code", true)]
        public static AchievementProgress UseCheatcode(Player player)
        {
            return new AchievementProgress(player.UsedCheatCode);
        }
    }
}