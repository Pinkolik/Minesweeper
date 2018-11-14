using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Minesweeper.Constants;
using Minesweeper.Engine;
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
        public static AchievementProgress LoseTenTimes(Player player, MineField field)
        {
            return new AchievementProgress(player.LostTimes >= 10, true, 10, player.LostTimes);
        }

        [AchievementInfo("Things take time", "Lose 500 times on any field")]
        public static AchievementProgress LoseFiveHundredTimes(Player player, MineField field)
        {
            return new AchievementProgress(player.LostTimes >= 500, true, 500, player.LostTimes);
        }


        [AchievementInfo("Never give up", "Lose 1000 times on any field")]
        public static AchievementProgress LoseThousandTimes(Player player, MineField field)
        {
            return new AchievementProgress(player.LostTimes >= 1000, true, 1000, player.LostTimes);
        }

        [AchievementInfo("First steps", "Win 10 times on any field")]
        public static AchievementProgress WinTenTimes(Player player, MineField field)
        {
            return new AchievementProgress(player.WonTimes >= 10, true, 10, player.WonTimes);
        }

        [AchievementInfo("Experienced minesweeper", "Win 500 times on any field")]
        public static AchievementProgress WinFiveHundredTimes(Player player, MineField field)
        {
            return new AchievementProgress(player.WonTimes >= 500, true, 500, player.WonTimes);
        }

        [AchievementInfo("Minesweeper guru", "Win 1000 times on any field")]
        public static AchievementProgress WinThousandTimes(Player player, MineField field)
        {
            return new AchievementProgress(player.WonTimes >= 1000, true, 1000, player.WonTimes);
        }

        [AchievementInfo("Beginner", "Win beginner field for the first time")]
        public static AchievementProgress WinBeginnerField(Player player, MineField field)
        {
            return new AchievementProgress(
                !double.IsPositiveInfinity(player.BestTimes[GameConstants.BeginnerSettings]));
        }

        [AchievementInfo("Semi-pro", "Win intermediate field for the first time")]
        public static AchievementProgress WinIntermediateField(Player player, MineField field)
        {
            return new AchievementProgress(
                !double.IsPositiveInfinity(player.BestTimes[GameConstants.IntermediateSettings]));
        }

        [AchievementInfo("Not bad", "Win intermediate field without using skills")]
        public static AchievementProgress WinIntermediateFieldWithoutUsingSkills(Player player, MineField field)
        {
            if (field == null) return new AchievementProgress(false);
            return new AchievementProgress(field.FieldSettings.Equals(GameConstants.IntermediateSettings) &&
                                           field.SkillsUsed == 0);
        }

        [AchievementInfo("Almost pro", "Win expert field for the first time")]
        public static AchievementProgress WinExpertField(Player player, MineField field)
        {
            return new AchievementProgress(!double.IsPositiveInfinity(player.BestTimes[GameConstants.ExpertSettings]));
        }

        [AchievementInfo("Super expert", "Win expert field without using skills")]
        public static AchievementProgress WinExpertFieldWithoutUsingSkills(Player player, MineField field)
        {
            if (field == null) return new AchievementProgress(false);
            return new AchievementProgress(field.FieldSettings.Equals(GameConstants.ExpertSettings) &&
                                           field.SkillsUsed == 0);
        }

        [AchievementInfo("Skilled player", "Use at least one skill")]
        public static AchievementProgress UseAtLeastOneSkill(Player player, MineField field)
        {
            return new AchievementProgress(player.SkillsUsed > 0);
        }

        [AchievementInfo("I like using skills", "Use 100 skills")]
        public static AchievementProgress UseOneHundredSkills(Player player, MineField field)
        {
            return new AchievementProgress(player.SkillsUsed >= 100);
        }

        [AchievementInfo("Fancy", "Buy at least one skin")]
        public static AchievementProgress BuyAtLeastOneSkin(Player player, MineField field)
        {
            return new AchievementProgress(player.OwnedSkins.Count > 1);
        }

        [AchievementInfo("Dandy", "Own all skins")]
        public static AchievementProgress OwnAllSkins(Player player, MineField field)
        {
            return new AchievementProgress(player.OwnedSkins.Count == GameConstants.Skins.Length, true,
                GameConstants.Skins.Length, player.OwnedSkins.Count);
        }

        //[AchievementInfo("Bad luck", "Explode on last mine on expert field", true)]
        //public static AchievementProgress ExplodeOnLastMine(Player player, MineField field)
        //{
        //    return new AchievementProgress(player.OwnedSkins.Count > 1);
        //}

        [AchievementInfo("Cheater", "Use \"XYZZY\" code", true)]
        public static AchievementProgress UseCheatcode(Player player, MineField field)
        {
            return new AchievementProgress(player.UsedCheatCode);
        }
    }
}