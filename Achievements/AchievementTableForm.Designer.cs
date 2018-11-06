namespace Minesweeper.Achievements
{
    partial class AchievementTableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.achievementsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // achievementsFlowLayoutPanel
            // 
            this.achievementsFlowLayoutPanel.AutoScroll = true;
            this.achievementsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.achievementsFlowLayoutPanel.Location = new System.Drawing.Point(12, 12);
            this.achievementsFlowLayoutPanel.Name = "achievementsFlowLayoutPanel";
            this.achievementsFlowLayoutPanel.Size = new System.Drawing.Size(499, 426);
            this.achievementsFlowLayoutPanel.TabIndex = 3;
            this.achievementsFlowLayoutPanel.WrapContents = false;
            // 
            // AchievementTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 450);
            this.Controls.Add(this.achievementsFlowLayoutPanel);
            this.Name = "AchievementTableForm";
            this.Text = "Achievements";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel achievementsFlowLayoutPanel;
    }
}