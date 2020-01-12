using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Composer
{
    class MusicStaff : Panel
    {
        private const int LineSpacing = 10;

        private readonly ToolStrip _ButtonPanel;
        private readonly ToolStripComboBox _TempoComboBox;
        private readonly SoundPlayer _Player;
        private ObservableCollection<MusicNote> _Notes;

        public ICollection<MusicNote> Notes
        {
            get
            {
                return _Notes;
            }

            private set
            {
                Controls.Clear();
                Controls.Add(_ButtonPanel);
                Controls.AddRange(value.ToArray());
            }
        }

        public MusicStaff()
        {
            _Notes = new ObservableCollection<MusicNote>();
            _Player = new SoundPlayer();
            _Notes.CollectionChanged += Notes_CollectionChanged;

            _ButtonPanel = new ToolStrip();
            _ButtonPanel.Dock = DockStyle.Bottom;

            _TempoComboBox = new ToolStripComboBox();
            _TempoComboBox.Items.AddRange(Enum.GetNames(typeof(Tempo)));
            _TempoComboBox.SelectedItem = "Allegro";

            _ButtonPanel.Items.Add(new ToolStripButton("Play"));
            _ButtonPanel.Items.Add(new ToolStripButton("Save"));
            _ButtonPanel.Items.Add(new ToolStripButton("Load"));
            _ButtonPanel.Items.Add(new ToolStripButton("Clear"));
            _ButtonPanel.Items.Add(new ToolStripSeparator());
            _ButtonPanel.Items.Add(new ToolStripLabel("Tempo"));
            _ButtonPanel.Items.Add(_TempoComboBox);

            _ButtonPanel.Items[0].Click += PlayButton_Click;
            _ButtonPanel.Items[1].Click += SaveButton_Click;
            _ButtonPanel.Items[2].Click += LoadButton_Click;
            _ButtonPanel.Items[3].Click += ClearButton_Click;

            Controls.Add(_ButtonPanel);

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (Notes.Count == 0)
            {
                MessageBox.Show("There are no notes to play", "Piano", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var t = (int)Enum.Parse(typeof(Tempo), _TempoComboBox.SelectedItem.ToString()) / 10.0;

            foreach (var x in Notes)
            {
                x.Play(_Player, t);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Music Files (.mus) | *.mus";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var stream = File.OpenWrite(dialog.FileName))
                        {
                            var formatter = new BinaryFormatter();

                            formatter.Serialize(stream, Notes);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("The application was unable to read this file due to insufficient file permissions", "Composer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "Music Files (.mus) | *.mus";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var stream = File.OpenRead(dialog.FileName))
                        {
                            var formatter = new BinaryFormatter();

                            Notes = formatter.Deserialize(stream) as ObservableCollection<MusicNote>;
                        }
                    }
                    catch (SerializationException)
                    {
                        MessageBox.Show("The application was unable to read this file. It may be corrupt", "Composer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("The application was unable to read this file due to insufficient file permissions", "Composer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("The application was unable to read this file", "Composer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This action will remove all notes from the staff. Would you like to continue?", "Composer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Notes.Clear();
            }
        }

        private void Notes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var x = e.NewItems[0] as MusicNote;
                var p = MusicInfo.NoteInfo[x.Pitch].Item2; // Staff Position
                var y = (LineSpacing * 9) - (p * 5) - x.Height + 1;

                x.Location = new Point((Notes.Count + 1) * 35, y);

                Controls.Add(x);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                var x = e.OldItems[0] as Control;

                Controls.Remove(x);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                for (var i = Controls.Count - 1; i > 0; i--)
                {
                    Controls.RemoveAt(i);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var gfx = e.Graphics;

            using (var pen1 = new Pen(Color.DarkGray, 1) { DashStyle = DashStyle.Dash })
            using (var pen2 = new Pen(Color.Black, 1))
            {
                gfx.DrawLine(pen1, 0, LineSpacing * 2, Width - 1, LineSpacing * 2);

                for (var i = 3; i <= 7; i++)
                {
                    gfx.DrawLine(pen2, 0, i * LineSpacing, Width - 1, i * LineSpacing);
                }

                gfx.DrawLine(pen1, 0, 8 * LineSpacing, Width - 1, 8 * LineSpacing);
            }

            gfx.DrawImage(Properties.Resources.Treble, new RectangleF(0, LineSpacing * 1.5f, 50, 75));
        }
    }
}
