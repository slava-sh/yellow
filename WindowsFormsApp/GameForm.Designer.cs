﻿using WindowsFormsApp.Views;

namespace WindowsFormsApp
{
    partial class GameForm
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
            this.gameView = new GameView();
            this.hardDropButton = new ButtonView();
            this.shiftRightButton = new ButtonView();
            this.shiftLeftButton = new ButtonView();
            this.softDropButton = new ButtonView();
            this.rotateButton = new ButtonView();
            ((System.ComponentModel.ISupportInitialize)(this.gameView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hardDropButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftRightButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftLeftButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.softDropButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotateButton)).BeginInit();
            this.SuspendLayout();
            // 
            // gameView
            // 
            this.gameView.Location = new System.Drawing.Point(0, 0);
            this.gameView.Name = "gameView";
            this.gameView.Size = new System.Drawing.Size(488, 731);
            this.gameView.TabIndex = 0;
            this.gameView.TabStop = false;
            // 
            // hardDropButton
            // 
            this.hardDropButton.Location = new System.Drawing.Point(323, 578);
            this.hardDropButton.Name = "hardDropButton";
            this.hardDropButton.Size = new System.Drawing.Size(100, 100);
            this.hardDropButton.TabIndex = 3;
            this.hardDropButton.Text = "hardDropButton";
            // 
            // shiftRightButton
            // 
            this.shiftRightButton.Location = new System.Drawing.Point(140, 618);
            this.shiftRightButton.Name = "shiftRightButton";
            this.shiftRightButton.Size = new System.Drawing.Size(60, 60);
            this.shiftRightButton.TabIndex = 5;
            this.shiftRightButton.Text = "shiftRightButton";
            // 
            // shiftLeftButton
            // 
            this.shiftLeftButton.Location = new System.Drawing.Point(39, 618);
            this.shiftLeftButton.Name = "shiftLeftButton";
            this.shiftLeftButton.Size = new System.Drawing.Size(60, 60);
            this.shiftLeftButton.TabIndex = 6;
            this.shiftLeftButton.Text = "shiftLeftButton";
            // 
            // softDropButton
            // 
            this.softDropButton.Location = new System.Drawing.Point(89, 661);
            this.softDropButton.Name = "softDropButton";
            this.softDropButton.Size = new System.Drawing.Size(60, 60);
            this.softDropButton.TabIndex = 7;
            this.softDropButton.Text = "softDropButton";
            // 
            // rotateButton
            // 
            this.rotateButton.Location = new System.Drawing.Point(89, 565);
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(60, 60);
            this.rotateButton.TabIndex = 8;
            this.rotateButton.Text = "rotateButton";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 846);
            this.Controls.Add(this.rotateButton);
            this.Controls.Add(this.softDropButton);
            this.Controls.Add(this.shiftLeftButton);
            this.Controls.Add(this.shiftRightButton);
            this.Controls.Add(this.hardDropButton);
            this.Controls.Add(this.gameView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yellow";
            ((System.ComponentModel.ISupportInitialize)(this.gameView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hardDropButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftRightButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftLeftButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.softDropButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotateButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GameView gameView;
        private ButtonView hardDropButton;
        private ButtonView shiftRightButton;
        private ButtonView shiftLeftButton;
        private ButtonView softDropButton;
        private ButtonView rotateButton;
    }
}
