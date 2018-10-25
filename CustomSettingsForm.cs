﻿using System;
using System.Windows.Forms;

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
                settings = mines >= columns * rows || columns <= 0 || rows <= 0 || mines <= 0
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