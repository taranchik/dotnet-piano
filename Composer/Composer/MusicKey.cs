using System.Drawing;
using System.Windows.Forms;

namespace Composer
{
    class MusicKey : Button
    {
        public int Pitch { get; set; }

        public MusicKey(bool isSemitone)
        {
            BackColor = isSemitone ? Color.Black : Color.White;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 1;
            FlatAppearance.BorderColor = Color.LightGray;
        }
    }
}
