using System;

namespace Minesweeper.Achievements
{
    public class AchievementEvenArgs : EventArgs
    {
        public AchievementEvenArgs(AchievementInfo achievementInfo)
        {
            AchievementInfo = achievementInfo;
        }

        public AchievementInfo AchievementInfo { get; }
    }
}