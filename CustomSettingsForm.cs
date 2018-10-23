using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Minesweeper.Properties;

namespace Minesweeper
{
    public partial class CustomSettingsForm : Form
    {

        public CustomSettingsForm()
        {
            InitializeComponent();
            MaximizeBox = false;
            var settings = SettingsLoader.TryLoadSettings();
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
                settings = mines > columns * rows
                    ? GameConstants.BeginnerSettings
                    : new FieldSettings(columns, rows, mines);
            }
            catch (FormatException)
            {
                settings = GameConstants.BeginnerSettings;
            }

            Serializer.Serialize(settings, GameConstants.SettingsFileName);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}