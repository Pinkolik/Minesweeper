using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Minesweeper.Constants;
using Minesweeper.PlayerNamespace;

namespace Minesweeper.Achievements
{
    public partial class StatisticsForm : Form
    {
        public StatisticsForm(Player player)
        {
            InitializeComponent();
            MaximizeBox = false;
            ShowPlayerStats(player);
        }

        private void ShowPlayerStats(Player player)
        {
            beginnerLabel.Text = "Beginner";
            beginnerValueLabel.Text = player.BestTimes[GameConstants.BeginnerSettings].ToString();

            intermediateLabel.Text = "Intermediate";
            intermediateValueLabel.Text = player.BestTimes[GameConstants.IntermediateSettings].ToString();


            expertLabel.Text = "Expert";
            expertValueLabel.Text = player.BestTimes[GameConstants.ExpertSettings].ToString();

            wonTimesLabel.Text = "Won times";
            wonTimesValueLabel.Text = player.WonTimes.ToString();

            lostTimesLabel.Text = "Lost times";
            lostTimesValueLabel.Text = player.LostTimes.ToString();

            moneyEarnedLabel.Text = "Money earned total";
            moneyEarnedValueLabel.Text = player.MoneyEarnedTotal.ToString();

            skilsUsedLabel.Text = "Skills used";
            skillsUsedValueLabel.Text = player.SkillsUsed.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}