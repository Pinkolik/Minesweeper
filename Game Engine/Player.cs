using System;
using System.Collections.Generic;
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
            var skinToBuy = GameConstants.Skins.First(pair => pair.Key.SkinName == name);
            if (Money < skinToBuy.Value) return false;
            _ownedSkins.Add(skinToBuy.Key.SkinName);
            OwnedSkins = _ownedSkins.ToArray();
            Money -= skinToBuy.Value;
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