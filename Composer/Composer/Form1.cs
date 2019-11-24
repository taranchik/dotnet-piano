using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Composer
{
    public partial class Form1 : Form
    {
        public int count = 0;
        int xLoc = 50;
        int yLoc = 30;

        public Form1()
        {
            InitializeComponent();
        }

        private void MusicNote_Click(object sender, EventArgs e)
        {
            //foreach(MusicNote mn in this.panel2.Controls) // panel2 is holder of Music staff
            //    if(sender == mn)    // true for the specific note clicked on Music staff
            //    {
            //        timer1.Enabled = true; // variable of the Timer component
            //        count = 0;
            //        SoundPlayer sp = new SoundPlayer();
            //        sp.SoundLocation(mk.notePitch.ToString + "way");
            //        while(ContainsFocus <= mn.noteDuration)
            //            sp.Play();
            //        timer1.Enabled = false;
            //        sp.Stop();
            //    }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
        }
    }
}
