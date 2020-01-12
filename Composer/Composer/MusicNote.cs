using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Forms;

namespace Composer
{
    [Serializable]
    public class MusicNote : PictureBox, ISerializable
    {
        // Private Fields
        private Point _DragStart;
        private bool _IsDragging = false;
        private int _Duration; // 1 Tick = 63ms

        public int Pitch { get; set; }
        public int Duration
        {
            get
            {
                return _Duration;
            }

            set
            {
                if (value >= 11 && value <= 15)
                {
                    _Duration = 13;
                    Image = Properties.Resources.DotMinim;
                }
                else if (value >= 6 && value <= 10)
                {
                    _Duration = 8;
                    Image = Properties.Resources.Minim;
                }
                else if (value >= 3 && value <= 5)
                {
                    _Duration = 4;
                    Image = Properties.Resources.Crotchet;
                }
                else if (value == 2)
                {
                    _Duration = 2;
                    Image = Properties.Resources.Quaver;
                }
                else if (value == 1)
                {
                    _Duration = 1;
                    Image = Properties.Resources.SemiQuaver;
                }
                else
                {
                    _Duration = 16;
                    Image = Properties.Resources.SemiBreve;
                }
            }
        }


        public MusicNote(int x, int pitch, int duration) : base()
        {
            Pitch = pitch;
            Duration = duration;
            Location = new Point(x, 50);
            Size = new Size(100, 160);

            InitializeComponent();
        }

        public MusicNote(SerializationInfo info, StreamingContext context)
        {
            Pitch = info.GetInt32("Pitch");
            Duration = info.GetInt32("Duration");
            Location = (Point)info.GetValue("Location", typeof(Point));

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            BackColor = Color.Transparent;
            SizeMode = PictureBoxSizeMode.AutoSize;

            // Events Handlers
            MouseDown += new MouseEventHandler(MusicNote_MouseDown);
            MouseUp += new MouseEventHandler(MusicNote_MouseUp);
            MouseMove += new MouseEventHandler(MusicNote_MouseMove);
            MouseClick += new MouseEventHandler(MusicNote_MouseClick);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            var gfx = pe.Graphics;

            if (MusicInfo.NoteInfo[Pitch].Item3) // Is Semitone
            {
                gfx.DrawString("#", SystemFonts.DefaultFont, Brushes.Black, 0, 20);
            }
        }

        private void MusicNote_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _IsDragging = true;
                _DragStart = e.Location;
            }
        }

        private void MusicNote_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _IsDragging = false;
            }
        }

        private void MusicNote_MouseMove(object sender, MouseEventArgs e)
        {
            if (_IsDragging && Math.Abs(_DragStart.Y - e.Y) > 10)
            {
                if (_DragStart.Y < e.Y && Pitch > 0 && ((Parent.Height - Bottom) >= 5)) // Dragging Down
                {
                    if (MusicInfo.NoteInfo[Pitch].Item3 != MusicInfo.NoteInfo[Pitch - 1].Item3) // Compare Staff Position
                    {
                        Location = new Point(Location.X, Location.Y + 5);
                    }

                    Pitch--;
                }
                else if (_DragStart.Y > e.Y && Pitch < (MusicInfo.NoteInfo.Length - 1) && Top >= 5) // Dragging Up
                {
                    if (MusicInfo.NoteInfo[Pitch].Item3 != MusicInfo.NoteInfo[Pitch + 1].Item3) // If Not Semitone
                    {
                        Location = new Point(Location.X, Location.Y - 5);
                    }

                    Pitch++;
                }
            }

            Invalidate();
        }

        private void MusicNote_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Play();
            }
            else if (e.Button == MouseButtons.Right)
            {
                var x = (Array.BinarySearch(MusicInfo.DurationScale, Duration) + 1) % MusicInfo.DurationScale.Length;

                Duration = MusicInfo.DurationScale[x];
            }
        }

        public void Play(SoundPlayer sfxPlayer, double spdModifier)
        {
            sfxPlayer.Stream = Properties.Resources.ResourceManager.GetObject(string.Format("_{0:D2}", Pitch)) as MemoryStream;
            sfxPlayer.Play();

            Thread.Sleep((int)(63 * spdModifier) * Duration);

            sfxPlayer.Stop();
        }

        public void Play()
        {
            using (var sfxPlayer = new SoundPlayer())
            {
                Play(sfxPlayer, 1);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Location", Location, typeof(Point));
            info.AddValue("Pitch", Pitch);
            info.AddValue("Duration", Duration);
        }
    }
}