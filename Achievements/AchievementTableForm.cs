using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Minesweeper.PlayerNamespace;

namespace Minesweeper.Achievements
{
    public partial class AchievementTableForm : Form
    {
        private const int FlowLayoutPanelHeight = 69;

        public AchievementTableForm(Player player)
        {
            InitializeComponent();
            ShowPlayerAchievements(player);
            MaximizeBox = false;
        }

        private void ShowPlayerAchievements(Player player)
        {
            foreach (var achievement in player.Achievements)
            {
                var achievementInfo = AchievementChecker.AchievementDictionary.Values
                    .First(info => info.Id == achievement.Key);

                var flowLayoutPanel = new FlowLayoutPanel();
                flowLayoutPanel.BorderStyle = BorderStyle.Fixed3D;
                flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
                //flowLayoutPanel.AutoSize = true;
                flowLayoutPanel.Height = FlowLayoutPanelHeight;
                flowLayoutPanel.MinimumSize = new Size(achievementsFlowLayoutPanel.Width - 30, 0);
                if (achievement.Value.Completed) flowLayoutPanel.BackColor = Color.LightGreen;

                var nameFont = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold);
                var nameLabel = new Label();
                nameLabel.AutoSize = true;
                nameLabel.Text = achievementInfo.Secret && !achievement.Value.Completed
                    ? "Secret achievement"
                    : achievementInfo.Name;
                nameLabel.Font = nameFont;
                nameFont.Dispose();

                var descriptionLabel = new Label();
                descriptionLabel.AutoSize = true;
                descriptionLabel.Text = achievementInfo.Secret && !achievement.Value.Completed
                    ? "Secret achievement"
                    : achievementInfo.Description;

                ProgressBar progressBar = null;
                if (!achievementInfo.Secret && !achievement.Value.Completed && achievement.Value.HasProgress)
                {
                    progressBar = new ProgressBar();
                    progressBar.Maximum = achievement.Value.Overall;
                    progressBar.Value = achievement.Value.Progress;
                    progressBar.Width = flowLayoutPanel.Width - 10;
                }

                flowLayoutPanel.Controls.AddRange(new Control[] {nameLabel, descriptionLabel, progressBar});
                achievementsFlowLayoutPanel.Controls.Add(flowLayoutPanel);
            }
        }
    }
}