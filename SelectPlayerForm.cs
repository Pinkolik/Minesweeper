using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class SelectPlayerForm : Form
    {
        public SelectPlayerForm()
        {
            InitializeComponent();
            MaximizeBox = false;
        }

        public Player SelectedPlayer { get; private set; }

        private void SelectPlayerForm_Load(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            playersListBox.Items.Clear();
            var players = PlayerLoader.LoadPlayers();
            foreach (var player in players)
                playersListBox.Items.Add(player);
        }

        private void newPlayerButton_Click(object sender, EventArgs e)
        {
            var newPlayerForm = new NewPlayerForm();
            newPlayerForm.ShowDialog();

            var players = PlayerLoader.LoadPlayers();
            players.Add(new Player(newPlayerForm.Nickname));
            Serializer.Serialize(players, GameConstants.PlayerFileName);
            RefreshList();
        }

        private void deletePlayerButton_Click(object sender, EventArgs e)
        {
            var player = (Player) playersListBox.SelectedItem;
            var players = PlayerLoader.LoadPlayers();
            players.Remove(player);
            Serializer.Serialize(players, GameConstants.PlayerFileName);
            RefreshList();
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (playersListBox.SelectedItem == null)
            {
                MessageBox.Show("You should select a player first");
                return;
            }

            SelectedPlayer = (Player) playersListBox.SelectedItem;
            Close();
        }
    }
}