using System;

namespace Minesweeper.Achievements
{
    public class AchievementInfo : Attribute
    {
        private static int _id;

        public AchievementInfo(string name, string description, bool secret = false)
        {
            Id = _id++;
            Name = name;
            Description = description;
            Secret = secret;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public bool Secret { get; }
    }
}