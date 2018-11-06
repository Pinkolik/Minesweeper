using System;

namespace Minesweeper.Achievements
{
    [Serializable]
    public class AchievementProgress
    {
        public AchievementProgress(bool completed, bool hasProgress = false, int overall = 0, int progress = 0)
        {
            Completed = completed;
            HasProgress = hasProgress;
            Overall = overall;
            Progress = Math.Min(progress, overall);
        }

        public bool Completed { get; }
        public bool HasProgress { get; }
        public int Overall { get; }
        public int Progress { get; }
    }
}