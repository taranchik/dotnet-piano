using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;

namespace Composer
{
    public enum Tempo
    {
        Grave = 0,
        Largo,
        Lento,
        Adagio,
        Andante,
        Moderato,
        Allegro,
        Presto
    }

    class MusicStaff : Panel
    {
        private Button _PlayButton;
        private Button _LoadButton;
        private Button _SaveButton;
        private ObservableCollection<MusicNote> _Notes;

        public ICollection<MusicNote> Notes { get => _Notes; }
        public Tempo Tempo { get; set; }

        public MusicStaff()
        {
            _Notes = new ObservableCollection<MusicNote>();
            _Notes.CollectionChanged += Notes_CollectionChanged;

            _Notes.Add(new MusicNote(50, 0, 1));
        }

        private void Notes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                Controls.Add(e.NewItems[0] as Control);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var x = Height / 5;
            var gfx = e.Graphics;

            using (var pen = new Pen(Color.Black, 1))
            {
                for (var i = 0; i < 5; i++)
                {
                    gfx.DrawLine(pen, 0, i * x, Width - 1, i * x);
                }
            }
        }
    }
}
