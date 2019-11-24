using System.Drawing;
using System.Windows.Forms;

namespace Composer
{
    public class MusicNote : PictureBox
    {
        private const string Root = @"Images\";

        //----------------- Data fields -----------------
        int pitch; // number of music key (i.e. sound frequency)
        int duration; // shape of note
        bool isDragging = false; // added field indentifying beginning and end of dragging

        //----------------- Constructor -----------------
        public MusicNote(int x, int iPitch, int iNoteShape) : base()
        {
            pitch = iPitch;
            duration = iNoteShape;
            Location = new Point(x, 50);   //  value of x specifies horizontal position of the music note picture
            Size = new Size(25, 40);

            //----------------- get image of music note -----------------
            Image = Properties.Resources.Crotchet;
            BackColor = Color.Transparent;

            //----------------- Mouse Event handlers registration -----------------
            MouseDown += new MouseEventHandler(StartDrag);
            MouseUp += new MouseEventHandler(StopDrag);
            MouseMove += new MouseEventHandler(NoteDrag);

            SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void InitializeComponent()
        {
            BackColor = SystemColors.Control;
            SizeMode = PictureBoxSizeMode.AutoSize;

            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            ResumeLayout(false);
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
