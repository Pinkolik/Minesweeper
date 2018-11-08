using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Windows.Forms;
using Minesweeper.Achievements;
using Minesweeper.Constants;
using Minesweeper.Engine;
using Minesweeper.PlayerNamespace;
using Minesweeper.SkillsNamespace;
using Minesweeper.Skins;

namespace Minesweeper.UI
{
    public partial class MainForm : Form
    {
        private Skills _chosenSkill = Skills.None;
        private FrameDimension _dimension;
        private int _frameIndex;
        private MineField _mineField;
        private Player _player;
        private FieldSettings _settings;
        private ISkin _skin;

        public MainForm()
        {
            InitializeComponent();
            MaximizeBox = false;
            _settings = SettingsLoader.TryLoadSettings();
            _skin = new DefaultSkin();
            SetFace(_skin.DefaultFace);
            InitializePlayer();
            timeTimer.Enabled = true;
        }

        private void ResizeForm()
        {
            var width = _settings.Columns * GameConstants.CellWidth;
            var height = _settings.Rows * GameConstants.CellHeight;
            ClientSize = new Size(width, height + menuStrip1.Height + facePictureBox.Height);
            facePictureBox.Top = menuStrip1.Height;
            facePictureBox.Left = (width - facePictureBox.Width) / 2;
            pictureBox1.Top = menuStrip1.Height + facePictureBox.Height;
            pictureBox1.Size = new Size(width, height);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            _mineField = new MineField(_settings);
            _mineField.OnGameOver += OnGameOver;
            _mineField.OnGameWon += OnGameWon;
            SetFace(_skin.DefaultFace);
            flagsLeftLabel.Text = _mineField.FlagsLeft.ToString();
            ResizeForm();
            pictureBox1.DrawField(_mineField, _skin);
        }

        private void OnMoneyChanged(object sender, EventArgs e)
        {
            Text = $"Minesweeper# | {_player.Name} ${_player.Money}";
        }

        private void OnAchievementUnlocked(object sender, EventArgs e)
        {
            var achievementEventArgs = (AchievementEvenArgs) e;
            MessageBox.Show($"Achievement {achievementEventArgs.AchievementInfo.Name} unlocked!");
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            pictureBox1.DrawField(_mineField, _skin);
            SetFace(_skin.LostFace);
            _player.ProcessField(_mineField);
            MessageBox.Show($"{_skin.LostMessage}\nTime: {Math.Floor(_mineField.TimeElapsed)}");
        }

        private void OnGameWon(object sender, EventArgs e)
        {
            pictureBox1.DrawField(_mineField, _skin);
            SetFace(_skin.WonFace);
            _player.ProcessField(_mineField);
            MessageBox.Show(
                $"{_skin.WinMessage}\nTime: {Math.Floor(_mineField.TimeElapsed)}\nMoney won: {_mineField.MoneyWon}");
        }

        private void SetFace(Bitmap face)
        {
            facePictureBox.Image = face;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_mineField == null) return;
            pictureBox1.HandleFieldClick(e, _mineField, _chosenSkill);
            _chosenSkill = Skills.None;
            pictureBox1.DrawField(_mineField, _skin);
            flagsLeftLabel.Text = _mineField.FlagsLeft.ToString();
            _mineField.CheckGameState();
        }

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = Math.Floor(_mineField.TimeElapsed).ToString();
            timeLabel.Left = ClientSize.Width - timeLabel.Width;
        }

        private void facePictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            StartNewGame();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            PlayerLoader.SavePlayer(_player);
            Serializer.Serializer.Serialize(_mineField.FieldSettings, GameConstants.SettingsFileName);
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mineField.Pause();
            pictureBox1.DrawField(_mineField, _skin);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            _player.KeyPress(e.KeyData.ToString());
        }

        #region Skills tool strip

        private void InitializeSkillsToolStrip()
        {
            skillsToolStripMenuItem.DropDownItems.Clear();
            foreach (var fieldInfo in typeof(Skills).GetFields())
            {
                var skillAttribute = (SkillInfo) fieldInfo.GetCustomAttribute(typeof(SkillInfo), false);
                if (skillAttribute == null) continue;
                var name = skillAttribute.Name;
                var cost = skillAttribute.Cost;
                if (name == "None") continue;
                var item = skillsToolStripMenuItem.DropDownItems.Add($"{name} {cost}");
                item.Click += (sender, args) => SkillClickHandler(sender, args, (Skills) fieldInfo.GetValue(null));
            }
        }

        private void SkillClickHandler(object sender, EventArgs eventArgs, Skills skill)
        {
            if (_mineField.GameState != GameState.InProgress)
            {
                MessageBox.Show("Game is not in progress.");
                return;
            }

            if (_player.BuySkill(skill))
                _chosenSkill = skill;
            else
                MessageBox.Show("Not enough money to buy this!");
        }

        #endregion

        #region Skin tool strip

        private void InitializeSkinToolStrip()
        {
            skinToolStripMenuItem.DropDownItems.Clear();
            foreach (var skin in GameConstants.Skins)
            {
                var skinAttribute = (SkinInfo) skin.GetType().GetCustomAttribute(typeof(SkinInfo), false);
                var id = skinAttribute.Id;
                var name = skinAttribute.Name;
                var cost = skinAttribute.Cost;
                var item = skinToolStripMenuItem.DropDownItems.Add(name);
                if (_player.OwnedSkins.Contains(id))
                {
                    item.Click += (sender, args) => PurchasedSkinClickHandler(sender, args, skin);
                }
                else
                {
                    item.Text += " " + cost;
                    item.Click += (sender, args) => NotPurchasedSkinClickHandler(sender, args, id);
                }
            }
        }

        private void PurchasedSkinClickHandler(object sender, EventArgs eventArgs, ISkin skin)
        {
            StopAnimating();
            _skin = skin;
            StartAnimating(_skin.Field);
            switch (_mineField.GameState)
            {
                case GameState.NotStarted:
                case GameState.InProgress:
                    SetFace(_skin.DefaultFace);
                    break;
                case GameState.Won:
                    SetFace(_skin.WonFace);
                    break;
                case GameState.Lost:
                    SetFace(_skin.LostFace);
                    break;
            }

            pictureBox1.DrawField(_mineField, _skin);
        }

        private void NotPurchasedSkinClickHandler(object sender, EventArgs eventArgs, int skinId)
        {
            if (_player.BuySkin(skinId))
                InitializeSkinToolStrip();
            else
                MessageBox.Show("Not enough money to buy this!");
        }

        #endregion

        #region Difficulty tool strip

        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settings = GameConstants.BeginnerSettings;
            StartNewGame();
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settings = GameConstants.IntermediateSettings;
            StartNewGame();
        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settings = GameConstants.ExpertSettings;
            StartNewGame();
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new CustomSettingsForm(_settings);
            settingsForm.ShowDialog();
            _settings = SettingsLoader.TryLoadSettings();
            StartNewGame();
        }

        #endregion

        #region Animation

        private void StartAnimating(Bitmap bitmap)
        {
            _dimension = new FrameDimension(bitmap.FrameDimensionsList[0]);
            _frameIndex = 0;
            gifTimer.Enabled = true;
        }

        private void StopAnimating()
        {
            gifTimer.Enabled = false;
        }

        private void gifTimer_Tick(object sender, EventArgs e)
        {
            _frameIndex = (_frameIndex + 1) % _skin.Field.GetFrameCount(_dimension);
            _skin.Field.SelectActiveFrame(_dimension, _frameIndex);
            pictureBox1.DrawField(_mineField, _skin);
        }

        #endregion

        #region Player tool strip

        private void InitializePlayer()
        {
            _player = PlayerLoader.GetPlayer() ?? _player;
            _player.OnMoneyChanged += OnMoneyChanged;
            _player.OnAchievementUnlocked += OnAchievementUnlocked;
            OnMoneyChanged(this, EventArgs.Empty);
            InitializeSkinToolStrip();
            InitializeSkillsToolStrip();
            StartNewGame();
        }

        private void changePlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayerLoader.SavePlayer(_player);
            InitializePlayer();
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var statsForm = new StatisticsForm(_player);
            statsForm.ShowDialog();
        }

        private void achievementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var achievementsListForm = new AchievementTableForm(_player);
            achievementsListForm.ShowDialog();
        }

        #endregion
    }
}