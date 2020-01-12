using System.Windows.Forms;

namespace Composer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            musicKeyboard.NotePlayed += NotePlayed;
        }

        private void NotePlayed(object sender, NoteEventArgs e)
        {
            musicStaff.Notes.Add(new MusicNote(50, e.Pitch, e.Duration));
        }
    }
}
