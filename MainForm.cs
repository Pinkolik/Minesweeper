using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public MainForm()
        {
            InitializeComponent();
            MaximizeBox = false;
            _settings = SettingsLoader.TryLoadSettings();
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
            pictureBox1.DrawField(_mineField);
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            MessageBox.Show($"Ты, бля, педик, проиграл, иди в жопу и анал {_mineField.TimeElapsed}");
        }

        private void OnGameWon(object sender, EventArgs e)
        {
            MessageBox.Show($"Ты, красавчик, заебись, разъебал всё поле {_mineField.TimeElapsed}");
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_mineField == null) return;
            pictureBox1.HandleFieldClick(e, _mineField);
            pictureBox1.DrawField(_mineField);
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
    }
}