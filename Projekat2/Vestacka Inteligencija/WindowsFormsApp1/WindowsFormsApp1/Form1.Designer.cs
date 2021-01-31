namespace WindowsFormsApp1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.lstbxCommands = new System.Windows.Forms.ListBox();
            this.tmrSpeaking = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lstbxCommands
            // 
            this.lstbxCommands.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lstbxCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbxCommands.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.lstbxCommands.FormattingEnabled = true;
            this.lstbxCommands.ItemHeight = 16;
            this.lstbxCommands.Location = new System.Drawing.Point(0, 0);
            this.lstbxCommands.Name = "lstbxCommands";
            this.lstbxCommands.Size = new System.Drawing.Size(394, 338);
            this.lstbxCommands.TabIndex = 0;
            // 
            // tmrSpeaking
            // 
            this.tmrSpeaking.Enabled = true;
            this.tmrSpeaking.Interval = 1000;
            this.tmrSpeaking.Tick += new System.EventHandler(this.tmrSpeaking_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(394, 338);
            this.Controls.Add(this.lstbxCommands);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstbxCommands;
        private System.Windows.Forms.Timer tmrSpeaking;
    }
}

