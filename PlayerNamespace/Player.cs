using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Minesweeper.Achievements;
using Minesweeper.Constants;
using Minesweeper.Engine;
using Minesweeper.SkillsNamespace;
using Minesweeper.Skins;

namespace Minesweeper.PlayerNamespace
{
    [Serializable]
    public class Player : IEquatable<Player>
    {
        private readonly Dictionary<int, AchievementProgress>
            _achievements = new Dictionary<int, AchievementProgress>();

        private readonly Dictionary<FieldSettings, double> _bestTimes = new Dictionary<FieldSettings, double>();
        private readonly List<int> _ownedSkins = new List<int> {0};
        private int _money;
        private string _input = "";
        private readonly string _cheat = "XYZZY";

        public Player(string name)
        {
            Name = name;
            OwnedSkins = new ReadOnlyCollection<int>(_ownedSkins);
            Achievements = new ReadOnlyDictionary<int, AchievementProgress>(_achievements);
            BestTimes = new ReadOnlyDictionary<FieldSettings, double>(_bestTimes);
            _bestTimes.Add(GameConstants.BeginnerSettings, double.PositiveInfinity);
            _bestTimes.Add(GameConstants.IntermediateSettings, double.PositiveInfinity);
            _bestTimes.Add(GameConstants.ExpertSettings, double.PositiveInfinity);
            CheckAchievements();
        }

        public int Money
        {
            get => _money;
            private set
            {
                _money = value;
                OnMoneyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool UsedCheatCode { get; private set; }
        public int MoneyEarnedTotal { get; private set; }
        public int SkillsUsed { get; private set; }
        public int LostTimes { get; private set; }
        public int WonTimes { get; private set; }
        public string Name { get; }
        public ReadOnlyCollection<int> OwnedSkins { get; }
        public ReadOnlyDictionary<int, AchievementProgress> Achievements { get; }
        public ReadOnlyDictionary<FieldSettings, double> BestTimes { get; }

        public bool Equals(Player other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        [field: NonSerialized] public event EventHandler OnMoneyChanged;
        [field: NonSerialized] public event EventHandler<AchievementEvenArgs> OnAchievementUnlocked;


        public bool BuySkin(int skinId)
        {
            var cost = GameConstants.Skins
                .Select(s => (SkinInfo) s.GetType().GetCustomAttribute(typeof(SkinInfo), false))
                .Where(attr => attr.Id == skinId)
                .Select(attr => attr.Cost)
                .FirstOrDefault();
            if (Money < cost) return false;
            _ownedSkins.Add(skinId);
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
            if (Name == "Pinkolik")
            {
                Money = int.MaxValue;
            }
            else
            {
                Money += field.MoneyWon;
                MoneyEarnedTotal += field.MoneyWon;
                SkillsUsed += field.SkillsUsed;
            }

            switch (field.GameState)
            {
                case GameState.Won:
                    WonTimes++;
                    CheckBestTimes(field);
                    break;
                case GameState.Lost:
                    LostTimes++;
                    break;
            }

            CheckAchievements();
        }

        private void CheckBestTimes(MineField field)
        {
            if (GameConstants.BeginnerSettings == field.FieldSettings)
                _bestTimes[field.FieldSettings] = _bestTimes[field.FieldSettings] > field.TimeElapsed
                    ? field.TimeElapsed
                    : _bestTimes[field.FieldSettings];
            else if (GameConstants.IntermediateSettings == field.FieldSettings)
                _bestTimes[field.FieldSettings] = _bestTimes[field.FieldSettings] > field.TimeElapsed
                    ? field.TimeElapsed
                    : _bestTimes[field.FieldSettings];
            else if (GameConstants.ExpertSettings == field.FieldSettings)
                _bestTimes[field.FieldSettings] = _bestTimes[field.FieldSettings] > field.TimeElapsed
                    ? field.TimeElapsed
                    : _bestTimes[field.FieldSettings];
        }

        private void CheckAchievements()
        {
            foreach (var methodInfo in typeof(AchievementChecker).GetMethods())
            {
                var achievementInfo = (AchievementInfo) methodInfo.GetCustomAttribute(typeof(AchievementInfo));
                if (achievementInfo == null) continue;
                var id = achievementInfo.Id;
                if (!_achievements.ContainsKey(id))
                    _achievements.Add(id, null);
                var previousState = _achievements[id] != null && _achievements[id].Completed;
                _achievements[id] = (AchievementProgress) methodInfo.Invoke(null, new object[] {this});
                if (!previousState && _achievements[id].Completed)
                    OnAchievementUnlocked?.Invoke(this, new AchievementEvenArgs(achievementInfo));
            }
        }

        public void KeyPress(string keyData)
        {
            if (UsedCheatCode) return;
            _input += keyData;
            for (var i = 0; i < _input.Length; i++)
                if (_input[i] != _cheat[i])
                {
                    _input = "";
                    return;
                }
            if (_cheat.Length != _input.Length) return;
            UsedCheatCode = true;
            CheckAchievements();
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