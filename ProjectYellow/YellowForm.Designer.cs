﻿namespace ProjectYellow
{
    partial class YellowForm
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
            this.SuspendLayout();
            // 
            // YellowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YellowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yellow";
            this.Load += new System.EventHandler(this.YellowForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.YellowForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.YellowForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

