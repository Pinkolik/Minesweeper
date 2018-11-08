using System;
using System.Windows.Forms;
using Minesweeper.Constants;

namespace Minesweeper.Engine
{
    public partial class CustomSettingsForm : Form
    {
        public CustomSettingsForm(FieldSettings settings)
        {
            InitializeComponent();
            MaximizeBox = false;
            widthTextBox.Text = settings.Columns.ToString();
            heightTextBox.Text = settings.Rows.ToString();
            minesTextBox.Text = settings.NumberOfMines.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            FieldSettings settings;
            try
            {
                var columns = int.Parse(widthTextBox.Text);
                var rows = int.Parse(heightTextBox.Text);
                var mines = int.Parse(minesTextBox.Text);
                settings = mines >= columns * rows || columns < 6 || rows < 6 || mines <= 0 || rows > 16 || columns > 32
                    ? GameConstants.BeginnerSettings
                    : new FieldSettings(columns, rows, mines);
            }
            catch (FormatException)
            {
                settings = GameConstants.BeginnerSettings;
            }

            Serializer.Serializer.Serialize(settings, GameConstants.SettingsFileName);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}