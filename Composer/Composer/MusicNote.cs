using System;
using System.Drawing;

namespace Composer
{
    public class MusicNote : PictureBox
    {
        public static string rootFolder = "E:\\Studia\\5 semester\\OOP\\assignment\\Composer\\Composer\\bin\\Debug\\MusicNotes-Images\\";
        //----------------- Data fields -----------------
        int pitch; // number of music key (i.e. sound frequency)
        int noteShape; // shape of note
        bool isDragging = false; // added field indentifying beginning and end of dragging
        //----------------- Constructor -----------------
        public MusicNote(int x, int iPitch, int inoteShape) : base()
        {
            pitch = iPitch;
            noteDuration = mDuration;
            Location = new Point(x, 200);   //  value of x specifies horizontal position of the music note picture
            this.Size = new size(25, 40);

            //----------------- get image of music note -----------------
            Bitmap bmp = new Bitmap(rootFolder + "SemiQuaver.bmp");
            this.Image = bmp;   //  Alternatively: this.Image = Image.FromFile(rootFolder + "SemiQuaver.bmp");
            this.BackColor = Color.Transparent;

            //----------------- Mouse Event handlers registration -----------------
            this.MouseDown += new MouseEventHandler(StartDrag);
            this.MouseUp += new MouseEventHandler(StopDrag);
            this.MouseMove += new MouseEventHandler(NoteDrag);
        }

        private void InitializeComponent()
        {
            this.BackColor = System.Drawing.SystemColor.Control;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
        //----------------- Mouse Event handlers implementation -----------------
        private void StartDrag(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                isDragging = true;
                pitch = e.Y; // current y coordinate of mouse
                this.Location = new Point(this.Location.X, pitch);
            }
        }

        private void StopDrag(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
                pitch = e.Y; // current y coordinate of mouse
            }
        }

        private void NoteDrag(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Top = this.Top + (e.Y - this.pitch); // move in VERTICAL direction
            }   //  Top property is distance in pixels between then top edge of the component and top edge of its container
        }

        protected override void OnPaint(PaintEventArgs pe)  //  redrawn automatically
        {
            base.OnPaint(pe);
        }
    }
}
