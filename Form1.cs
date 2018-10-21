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
    public partial class Form1 : Form
    {
        private MineField _mineField;
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.ClientSize = new Size(300, 300 + menuStrip1.Height);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mineField = new MineField(10, 10, 10);
            _mineField.OnGameOver += OnGameOver;
            _mineField.OnGameWon += OnGameWon;
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
    }
}
