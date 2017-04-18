using WindowsFormsApp.Views;

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
            this.screenView = new WindowsFormsApp.Views.ScreenView();
            this.hardDropButton = new WindowsFormsApp.Views.ButtonView();
            this.shiftRightButton = new WindowsFormsApp.Views.ButtonView();
            this.shiftLeftButton = new WindowsFormsApp.Views.ButtonView();
            this.softDropButton = new WindowsFormsApp.Views.ButtonView();
            this.rotateButton = new WindowsFormsApp.Views.ButtonView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.screenView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hardDropButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftRightButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftLeftButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.softDropButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotateButton)).BeginInit();
            this.SuspendLayout();
            // 
            // screenView
            // 
            this.screenView.Location = new System.Drawing.Point(54, 34);
            this.screenView.Name = "screenView";
            this.screenView.Size = new System.Drawing.Size(390, 509);
            this.screenView.TabIndex = 0;
            this.screenView.TabStop = false;
            // 
            // hardDropButton
            // 
            this.hardDropButton.Location = new System.Drawing.Point(309, 594);
            this.hardDropButton.Name = "hardDropButton";
            this.hardDropButton.Size = new System.Drawing.Size(100, 100);
            this.hardDropButton.TabIndex = 3;
            this.hardDropButton.Text = "hardDropButton";
            // 
            // shiftRightButton
            // 
            this.shiftRightButton.Location = new System.Drawing.Point(177, 615);
            this.shiftRightButton.Name = "shiftRightButton";
            this.shiftRightButton.Size = new System.Drawing.Size(60, 60);
            this.shiftRightButton.TabIndex = 5;
            this.shiftRightButton.Text = "shiftRightButton";
            // 
            // shiftLeftButton
            // 
            this.shiftLeftButton.Location = new System.Drawing.Point(70, 615);
            this.shiftLeftButton.Name = "shiftLeftButton";
            this.shiftLeftButton.Size = new System.Drawing.Size(60, 60);
            this.shiftLeftButton.TabIndex = 6;
            this.shiftLeftButton.Text = "shiftLeftButton";
            // 
            // softDropButton
            // 
            this.softDropButton.Location = new System.Drawing.Point(124, 665);
            this.softDropButton.Name = "softDropButton";
            this.softDropButton.Size = new System.Drawing.Size(60, 60);
            this.softDropButton.TabIndex = 7;
            this.softDropButton.Text = "softDropButton";
            // 
            // rotateButton
            // 
            this.rotateButton.Location = new System.Drawing.Point(124, 565);
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(60, 60);
            this.rotateButton.TabIndex = 8;
            this.rotateButton.Text = "rotateButton";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(120, 541);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 21);
            this.label1.TabIndex = 9;
            this.label1.Text = "ROTATE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(35, 604);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 21);
            this.label2.TabIndex = 10;
            this.label2.Text = "LEFT";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(229, 604);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "RIGHT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(124, 728);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 21);
            this.label4.TabIndex = 12;
            this.label4.Text = "DOWN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(333, 570);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 21);
            this.label5.TabIndex = 13;
            this.label5.Text = "DROP";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.ClientSize = new System.Drawing.Size(484, 768);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rotateButton);
            this.Controls.Add(this.softDropButton);
            this.Controls.Add(this.shiftLeftButton);
            this.Controls.Add(this.shiftRightButton);
            this.Controls.Add(this.hardDropButton);
            this.Controls.Add(this.screenView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yellow";
            ((System.ComponentModel.ISupportInitialize)(this.screenView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hardDropButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftRightButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shiftLeftButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.softDropButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotateButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScreenView screenView;
        private ButtonView hardDropButton;
        private ButtonView shiftRightButton;
        private ButtonView shiftLeftButton;
        private ButtonView softDropButton;
        private ButtonView rotateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

