using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MainForm : Form
    {
        private MineField _mineField;
        private FieldSettings _settings;
        private ISkin _skin;
        private FrameDimension _dimension;
        private int _frameIndex;

        public MainForm()
        {
            InitializeComponent();
            MaximizeBox = false;
            _settings = SettingsLoader.TryLoadSettings();
            _skin = new DefaultSkin();
            StartNewGame();
        }

        private void ResizeForm()
        {
            var width = _settings.Columns * GameConstants.CellWidth;
            var height = _settings.Rows * GameConstants.CellHeight;
            ClientSize = new Size(width, height + menuStrip1.Height);
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

        private void OnCustomSettingsClosed(object sender, EventArgs e)
        {
            _settings = SettingsLoader.TryLoadSettings();
            StartNewGame();
        }

        private void StartNewGame()
        {
            _mineField = new MineField(_settings);
            _mineField.OnGameOver += OnGameOver;
            _mineField.OnGameWon += OnGameWon;
            ResizeForm();
            pictureBox1.DrawField(_mineField, _skin);
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            pictureBox1.DrawField(_mineField, _skin);
            MessageBox.Show($"You've lost :( Time: {Math.Floor(_mineField.TimeElapsed)}");
        }

        private void OnGameWon(object sender, EventArgs e)
        {
            pictureBox1.DrawField(_mineField, _skin);
            MessageBox.Show($"You've won! Time: {Math.Floor(_mineField.TimeElapsed)}");
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_mineField == null) return;
            pictureBox1.HandleFieldClick(e, _mineField);
            pictureBox1.DrawField(_mineField, _skin);
            _mineField.CheckGameState();
        }

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
            var settingsForm = new CustomSettingsForm();
            settingsForm.FormClosed += OnCustomSettingsClosed;
            settingsForm.Show();
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopAnimating();
            _skin = new DefaultSkin();
            pictureBox1.DrawField(_mineField, _skin);
        }

        private void shrekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopAnimating();
            _skin = new ShrekSkin();
            pictureBox1.DrawField(_mineField, _skin);
        }

        private void mLGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopAnimating();
            _skin = new MLGSkin();
            StartAnimating(Properties.Resources.MLGField);
            pictureBox1.DrawField(_mineField, _skin);
        }

        private void minecraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopAnimating();
            _skin = new MinecraftSkin();
            pictureBox1.DrawField(_mineField, _skin);
        }

        private void falloutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _skin = new FalloutSkin();
            pictureBox1.DrawField(_mineField, _skin);
        }

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
    }
}