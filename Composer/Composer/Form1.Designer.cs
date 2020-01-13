namespace Composer
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
            this.musicKeyboard = new Composer.MusicKeyboard();
            this.musicStaff = new Composer.MusicStaff();
            this.SuspendLayout();
            // 
            // musicKeyboard
            // 
            this.musicKeyboard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.musicKeyboard.Location = new System.Drawing.Point(50, 278);
            this.musicKeyboard.Name = "musicKeyboard";
            this.musicKeyboard.Size = new System.Drawing.Size(700, 160);
            this.musicKeyboard.TabIndex = 1;
            // 
            // musicStaff
            // 
            this.musicStaff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.musicStaff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.musicStaff.Location = new System.Drawing.Point(12, 12);
            this.musicStaff.Name = "musicStaff";
            this.musicStaff.Size = new System.Drawing.Size(776, 240);
            this.musicStaff.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.musicKeyboard);
            this.Controls.Add(this.musicStaff);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Composer";
            this.ResumeLayout(false);

        }

        #endregion

        private MusicStaff musicStaff;
        private MusicKeyboard musicKeyboard;
    }
}

