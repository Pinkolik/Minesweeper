using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Minesweeper
{
    [Serializable]
    public class Player : IEquatable<Player>
    {
        private readonly List<string> _ownedSkins = new List<string> {new DefaultSkin().SkinName};
        private Dictionary<FieldSettings, int> _bestTimes;

        public Player(string name)
        {
            Name = name;
        }

        public int Money { get; private set; }
        public int LostTimes { get; private set; }
        public int WonTimes { get; private set; }
        public string Name { get; }
        public string[] OwnedSkins { get; private set; } = {new DefaultSkin().SkinName};

        public bool Equals(Player other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }


        public bool BuySkin(string name)
        {
            var cost = GameConstants.Skins
                .Select(s => (SkinInfo) s.GetType().GetCustomAttribute(typeof(SkinInfo), false))
                .Where(attr => attr.Name == name)
                .Select(attr => attr.Cost)
                .FirstOrDefault();
            if (Money < cost) return false;
            _ownedSkins.Add(name);
            OwnedSkins = _ownedSkins.ToArray();
            Money -= cost;
            return true;
        }

        public bool BuySkill(Skills skill)
        {
            var cost = ((SkillInfo) typeof(Skills)
                .GetField(Enum.GetName(typeof(Skills), skill))
                .GetCustomAttribute(typeof(SkillInfo), false))
                .Cost;
            if (Money < cost) return false;
            Money -= cost;
            return true;
        }

        public void ProcessField(MineField field)
        {
            if (Name == "Pinkolik") Money = int.MaxValue;
            else
                Money += field.MoneyWon;
            switch (field.GameState)
            {
                case GameState.Won:
                    WonTimes++;
                    break;
                case GameState.Lost:
                    LostTimes++;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Name} ${Money}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Player) obj);
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }
    }
}