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
            this.ControlBox = false;
            InitializeComponent();
            if (player)
            {
                youwonLabel.Visible = true;
            }
            else
            {
                youlostLabel.Visible = true;
            }

            if (endSession)
            {
                youwonLabel.Visible = false;
                youlostLabel.Visible = false;
                label5.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = true;
            }
            label2.Text = "Wins: " + gs.Wins;
            label3.Text = "Losses: " + gs.Losses;
            label6.Text = gs.TimeFormatted();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Game g = (Game)this.Owner;
            g.Replay();
            this.Dispose();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Game g = (Game)this.Owner;
            g.EndSession();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Owner.Dispose();
            this.Dispose();
        }
    }
}
