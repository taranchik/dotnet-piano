using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Composer
{
    class NoteEventArgs : EventArgs
    {
        public int Pitch { get; private set; }
        public int Duration { get; private set; }

        public NoteEventArgs(int pitch, int duration)
        {
            Pitch = pitch;
            Duration = duration;
        }
    }

    class MusicKeyboard : Panel
    {
        // Private Fields
        private readonly SoundPlayer _Player;
        private readonly System.Timers.Timer _Timer;
        private int _ElapsedTicks;

        private ValueTuple<bool, int>[] _KeyMap = new ValueTuple<bool, int>[] // <Is Semitone, Nearest Snap Point>
        {
            new ValueTuple<bool, int>(false, 0), new ValueTuple<bool, int>(true, 0),
            new ValueTuple<bool, int>(false, 1), new ValueTuple<bool, int>(true, 1),
            new ValueTuple<bool, int>(false, 2),
            new ValueTuple<bool, int>(false, 3), new ValueTuple<bool, int>(true, 3),
            new ValueTuple<bool, int>(false, 4), new ValueTuple<bool, int>(true, 4),
            new ValueTuple<bool, int>(false, 5), new ValueTuple<bool, int>(true, 5),
            new ValueTuple<bool, int>(false, 6),
        };

        // Public Fields
        public event EventHandler<NoteEventArgs> NotePlayed;
        
        public MusicKeyboard()
        {
            _Player = new SoundPlayer();
            _Timer = new System.Timers.Timer();

            _Timer.Elapsed += (sender, e) => { _ElapsedTicks++; };
            _Timer.Interval = 63;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < _KeyMap.Length; j++)
                {
                    var n = (i * _KeyMap.Length) + j;
                    var x = new MusicKey(_KeyMap[j].Item1) { Pitch = n, FlatStyle = FlatStyle.Standard };

                    if (!_KeyMap[j].Item1)
                    {
                        x.Width = 50;
                        x.Height = Height;
                        x.Location = new Point(_KeyMap[j].Item2 * 50, 0);
                        x.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                    }
                    else // If Semitone
                    {
                        x.Width = 35;
                        x.Height = Height / 2;
                        x.Location = new Point((_KeyMap[j].Item2 * 50) + 32, 0);
                        x.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                    }

                    x.MouseDown += Key_MouseDown;
                    x.MouseUp += Key_MouseUp;

                    Controls.Add(x);

                    if (_KeyMap[j].Item1) x.BringToFront();

                    _KeyMap[j].Item2 += 7;
                }
            }

            _KeyMap = null;
        }

        private void Key_MouseUp(object sender, MouseEventArgs e)
        {
            var x = (sender as MusicKey).Pitch;

            // Stop Timer
            _Timer.Stop();
            _Player.Stop();

            // Notify Listeners
            NotePlayed?.Invoke(this, new NoteEventArgs(x, _ElapsedTicks));
        }

        private void Key_MouseDown(object sender, MouseEventArgs e)
        {
            var x = (sender as MusicKey).Pitch;

            // Play Note
            _Player.Stream = Properties.Resources.ResourceManager.GetObject(MusicInfo.NoteInfo[x].Item1) as MemoryStream;
            _Player.Play();

            // Start Timer
            _ElapsedTicks = 0;
            _Timer.Start();
        }
    }
}
