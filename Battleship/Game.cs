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
    public partial class Game : Form
    {
        private String[] letters = new String[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        private List<String> availableSquares = new List<String>();

        public Game()
        {
            InitializeComponent();
            InitializeBoards();
            foreach (String i in letters)
            {
                for (int j = 1; j <= 10; j++)
                {
                    availableSquares.Add(i + j);
                }
            }

            Console.WriteLine(Controls.Count);
        }


        private void InitializeBoards()
        {
            Random rnd = new Random();
            BoatLocations playerBoatLocations = PickLocations(rnd);
            BoatLocations botBoatLocations = PickLocations(rnd);


            Board playerBoard = new Board(true, playerBoatLocations);
            playerBoard.Name = "playerBoard";
            //playerBoard.PlayerAction += PlayerAction;
            Board botBoard = new Board(false, botBoatLocations);
            botBoard.Name = "botBoard";

            playerBoard.Location = new Point(40, 40);
            botBoard.Location = new Point(650, 40);

            this.Controls.Add(playerBoard);
            this.Controls.Add(botBoard);
        }

        public void PlayerAction(string s, string sq)
        {
            Board bb = (Board)Controls["botBoard"];
            messageHistroy.Text = lastMessage.Text + "\n" + messageHistroy.Text;
            lastMessage.Text = "You fired at " + sq + ", it was a " + s + "\n";
            string ans = IsShipDestroyed(bb, sq);
            if (ans != "")
            {
                lastMessage.Text += "Bot's " + ans + " got destroyed \n";
                bb.BoatLocations.Destroyed[ans] = true;
            }
            BotAction();
        }

        private void BotAction()
        {
            Board pb = (Board)Controls["playerBoard"];
            Random rnd = new Random();
            int i = rnd.Next(availableSquares.Count);
            string square = availableSquares[i];
            availableSquares.Remove(square);
            if (pb.BoatLocations.AircraftCarrier.Contains(square) ||
                pb.BoatLocations.Destroyer.Contains(square) || pb.BoatLocations.Warship.Contains(square) ||
                pb.BoatLocations.Submarine.Contains(square))
            {
                Controls["playerBoard"].Controls[square].Text = "X";
                lastMessage.Text += "The Bot fired at " + square + ", it was a hit\n";
                string ans = IsShipDestroyed(pb, square);
                if (ans != "")
                {
                    lastMessage.Text += "Player's " + ans + " got destroyed \n";
                    pb.BoatLocations.Destroyed[ans] = true;
                }
            }
            else
            {
                lastMessage.Text += "The Bot fired at " + square + ", it was a miss\n";
                Controls["playerBoard"].Controls[square].Text = "-";
            }
            Console.WriteLine(square);
            
        }

        private String IsShipDestroyed(Board pb, String sq)
        {
            String returnString = "";
            BoatLocations bl = pb.BoatLocations;
            if (bl.AircraftCarrier.Contains(sq))
            {
                returnString = "AircraftCarrier";
                foreach (string s in bl.AircraftCarrier)
                {
                    if (availableSquares.Contains(s))
                    {
                        returnString = "";
                    }
                }
            } else if (bl.Destroyer.Contains(sq))
            {
                returnString = "Destroyer";
                foreach (string s in bl.Destroyer)
                {
                    if (availableSquares.Contains(s))
                    {
                        returnString = "";
                    }
                }
            } else if (bl.Warship.Contains(sq))
            {
                returnString = "Warship";
                foreach (string s in bl.Warship)
                {
                    if (availableSquares.Contains(s))
                    {
                        returnString = "";
                    }
                }
            }
            else if (bl.Submarine.Contains(sq))
            {
                returnString = "Submarine";
                foreach (string s in bl.Submarine)
                {
                    if (availableSquares.Contains(s))
                    {
                        returnString = "";
                    }
                }
            }
            return returnString;
        }

        private BoatLocations PickLocations(Random rnd)
        {
            
            BoatLocations boatLocations = new BoatLocations();
            // Aircraft Carrier
            {
                if (rnd.Next(0, 2) == 0) //horizontal 
                {
                    int row = rnd.Next(1, 11);
                    int startingCol = rnd.Next(1, 6);
                    for (int i = 0; i < 5; i++)
                    {
                        boatLocations.AircraftCarrier[i] = letters[row-1] + (startingCol + i);
                    }
                }
                else // vertical
                {
                    int col = rnd.Next(1, 11);
                    int startingRow = rnd.Next(1, 6);
                    for (int i = 0; i < 5; i++)
                    {
                        boatLocations.AircraftCarrier[i] = letters[i] + (col);
                    }
                }
            }
            // Destroyer (anti-torpedo ship)
            {
                if (rnd.Next(0, 2) == 0) //horizontal 
                {
                    Console.WriteLine("here");
                    while (boatLocations.CheckDestroyer())
                    {
                        Console.WriteLine("fail 1");
                        int row = rnd.Next(1, 11);
                        int startingCol = rnd.Next(1, 7);
                        for (int i = 0; i < 4; i++)
                        {
                            boatLocations.Destroyer[i] = letters[row-1] + (startingCol + i);
                        }
                    }
                }
                else // vertical
                {
                    while (boatLocations.CheckDestroyer())
                    {
                        Console.WriteLine("fail 1");

                        int col = rnd.Next(1, 11);
                        int startingRow = rnd.Next(1, 7);
                        for (int i = 0; i < 4; i++)
                        {
                            boatLocations.Destroyer[i] = letters[i] + (col);
                        }
                    }
                }
            }
            // Warship
            {
                if (rnd.Next(0, 2) == 0) //horizontal 
                {
                    while (boatLocations.CheckWarship())
                    {
                        Console.WriteLine("fail 2");
                        int row = rnd.Next(1, 11);
                        int startingCol = rnd.Next(1, 8);
                        for (int i = 0; i < 3; i++)
                        {
                            boatLocations.Warship[i] = letters[row-1] + (startingCol + i);
                        }
                    }
                }
                else // vertical
                {
                    while (boatLocations.CheckWarship())
                    {
                        Console.WriteLine("fail 2");
                        int col = rnd.Next(1, 11);
                        int startingRow = rnd.Next(1, 8);
                        for (int i = 0; i < 3; i++)
                        {
                            boatLocations.Warship[i] = letters[i] + (col);
                        }
                    }
                }
            }
            // Submarine
            {
                if (rnd.Next(0, 2) == 0) //horizontal 
                {
                    while (boatLocations.CheckSub())
                    {
                        Console.WriteLine("fail 3 ");
                        int row = rnd.Next(1, 11);
                        int startingCol = rnd.Next(1, 9);
                        for (int i = 0; i < 2; i++)
                        {
                            boatLocations.Submarine[i] = letters[row - 1] + (startingCol + i);
                        }
                    }
                }
                else // vertical
                {
                    while (boatLocations.CheckSub())
                    {
                        Console.WriteLine("fail 3");
                        int col = rnd.Next(1, 11);
                        int startingRow = rnd.Next(1, 9);
                        for (int i = 0; i < 2; i++)
                        {
                            boatLocations.Submarine[i] = letters[i] + (col);
                        }
                    }
                }
            }

            return boatLocations;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Controls.RemoveByKey("playerBoard");
            //Controls.RemoveByKey("botBoard");
            Controls["playerBoard"].Dispose();
            Controls["botBoard"].Dispose();
            InitializeBoards();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Panel panel = (Panel)Controls["botBoard"];
            foreach (Button button in panel.Controls.OfType<Button>())
            {
                button.PerformClick();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BotAction();
        }
    }
}
