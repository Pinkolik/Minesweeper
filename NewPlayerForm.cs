using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class NewPlayerForm : Form
    {
        public NewPlayerForm()
        {
            InitializeComponent();
            MaximizeBox = false;
        }

        public string Nickname { get; private set; }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (nicknameTextBox.Text == "") return;
            Nickname = nicknameTextBox.Text;
            Close();
        }
    }
}