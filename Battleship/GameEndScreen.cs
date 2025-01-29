using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class GameEndScreen : Form
    {
        public GameEndScreen(GameStats gs, bool player, bool endSession)
        {
            InitializeComponent();
            if (player)
            {
                youwonLabel.Visible = true;
            }
            else
            {
                youlostLabel.Visible = false;
            }

            if (endSession)
            {
                label5.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
            }
            label2.Text = "Wins: " + gs.Wins;
            label3.Text = "Losses: " + gs.Losses;
            int mins = (gs.Time / 60);
            label6.Text = "Time: " + mins + ":" + (gs.Time - (mins * 60));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game g = (Game)this.Parent;
            g.Replay();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Game g = (Game)this.Parent;
            g.EndSession();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Parent.Dispose();
            this.Dispose();
        }
    }
}
