using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Battleship
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name;
            if (textBox1.Text == string.Empty)
            {
                name = "Anonymous Player";
            }
            else
            {
                name = textBox1.Text;

            }
            Game game = new Game(name);
            game.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
