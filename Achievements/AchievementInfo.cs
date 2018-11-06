using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Achievements
{
    public class AchievementInfo : Attribute
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public bool Secret { get; }

        public AchievementInfo(int id, string name, string description, bool secret = false)
        {
            Id = id;
            Name = name;
            Description = description;
            Secret = secret;
        }
    }
}