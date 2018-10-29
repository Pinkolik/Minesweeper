using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MainForm : Form
    {
        private readonly Player _player;
        private FrameDimension _dimension;
        private int _frameIndex;
        private MineField _mineField;
        private FieldSettings _settings;
        private Skin _skin;

        public MainForm()
        {
            InitializeComponent();
            MaximizeBox = false;
            _player = PlayerLoader.GetPlayer();
            _settings = SettingsLoader.TryLoadSettings();
            _skin = new DefaultSkin();
            SetFace(_skin.DefaultFace);
            InitializeSkinToolStrip();
            StartNewGame();
            Text = $"Minesweeper# | {_player.Name} ${_player.Money}";
            timeTimer.Enabled = true;
        }

        private void InitializeSkinToolStrip()
        {
            skinToolStripMenuItem.DropDownItems.Clear();
            foreach (var skin in GameConstants.Skins)
            {
                var item = skinToolStripMenuItem.DropDownItems.Add(skin.Key.SkinName);
                if (_player.OwnedSkins.Contains(skin.Key.SkinName))
                {
                    item.Click += (sender, args) =>
                    {
                        StopAnimating();
                        _skin = skin.Key;
                        StartAnimating(_skin.Field);
                        SetFace(_skin.DefaultFace);
                        pictureBox1.DrawField(_mineField, _skin);
                    };
                }
                else
                {
                    item.Text += " " + skin.Value;
                    item.Click += (sender, args) =>
                    {
                        if (_player.BuySkin(skin.Key.SkinName))
                        {
                            InitializeSkinToolStrip();
                            Text = $"Minesweeper# | {_player.Name} ${_player.Money}";
                        }
                        else
                        {
                            MessageBox.Show("Not enough money to buy this!");
                        }
                    };
                }
            }
        }

        private void ResizeForm()
        {
            var width = _settings.Columns * GameConstants.CellWidth;
            var height = _settings.Rows * GameConstants.CellHeight;
            ClientSize = new Size(width, height + menuStrip1.Height + facePictureBox.Height);
            flagsLeftLabel.Text = string.Format("Flags left: {0}", _mineField.FlagsLeft());
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
            ResizeForm();
            pictureBox1.DrawField(_mineField, _skin);
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
            Text = $"Minesweeper# | {_player.Name} ${_player.Money}";
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
            pictureBox1.HandleFieldClick(e, _mineField);
            pictureBox1.DrawField(_mineField, _skin);
            flagsLeftLabel.Text = string.Format("Flags left: {0}", _mineField.FlagsLeft());
            _mineField.CheckGameState();
        }

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = "Time: " + Math.Floor(_mineField.TimeElapsed);
            timeLabel.Left = ClientSize.Width - timeLabel.Width;
        }

        private void facePictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            StartNewGame();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var players = PlayerLoader.LoadPlayers();
            players.Remove(_player);
            players.Add(_player);
            Serializer.Serialize(players, GameConstants.PlayerFileName);
        }

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
    }
}