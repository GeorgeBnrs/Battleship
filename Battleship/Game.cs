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
        private List<String> availableSquaresBot = new List<String>();
        private List<String> availableSquaresPlayer = new List<String>();
        
        private GameStats gs = new GameStats();

        public Game(string name)
        {
            InitializeComponent();
            gs.PlayerName = name;
            this.Text = "Battleship : " + gs.PlayerName; 
            InitializeBoards();
            foreach (String i in letters)
            {
                for (int j = 1; j <= 10; j++)
                {
                    availableSquaresBot.Add(i + j);
                    availableSquaresPlayer.Add(i + j);
                }
            }
        }


        private void InitializeBoards()
        {
            Random rnd = new Random();
            BoatLocations playerBoatLocations = PickLocations(rnd);
            BoatLocations botBoatLocations = PickLocations(rnd);
            Console.WriteLine(botBoatLocations);

            Board playerBoard = new Board(true, playerBoatLocations);
            playerBoard.Name = "playerBoard";
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
            if (s == "hit")
            {
                availableSquaresPlayer.Remove(sq);
                lastMessage.Text = "You fired at " + sq + ", it was a hit\n";
                string ans = IsShipDestroyed(bb, sq, true);
                if (ans != "")
                {
                    lastMessage.Text += "Bot's " + ans + " got destroyed \n";
                    bb.BoatLocations.Destroyed[ans] = true;
                    
                    if (ans == "AircraftCarrier")
                    {
                        foreach (string i in bb.BoatLocations.AircraftCarrier)
                        {
                            bb.Controls[i].BackColor = Color.GreenYellow;
                        }
                    } else if (ans == "Destroyer")
                    {
                        foreach (string i in bb.BoatLocations.Destroyer)
                        {
                            bb.Controls[i].BackColor = Color.MediumPurple;
                        }
                    } else if (ans == "Warship")
                    {
                        foreach (string i in bb.BoatLocations.Warship)
                        {
                            bb.Controls[i].BackColor = Color.Green;
                        }
                    } else if (ans == "Submarine")
                    {
                        foreach (string i in bb.BoatLocations.Submarine)
                        {
                            bb.Controls[i].BackColor = Color.DarkBlue;
                        }
                    }
                    if (bb.BoatLocations.allShipsDestroyed())
                    {
                        lastMessage.Text += "Game Ended, " + gs.PlayerName+ " won!";
                        gs.Wins++;
                        EndGame(true);
                    }
                }
            }
            else
            {
                lastMessage.Text = "You fired at " + sq + ", it was a miss\n";
            }

            BotAction();
        }

        private void BotAction()
        {
            Board pb = (Board)Controls["playerBoard"];
            Random rnd = new Random();
            int i = rnd.Next(availableSquaresBot.Count);
            string sq = availableSquaresBot[i];
            availableSquaresBot.Remove(sq);
            if (pb.BoatLocations.AircraftCarrier.Contains(sq) ||
                pb.BoatLocations.Destroyer.Contains(sq) || pb.BoatLocations.Warship.Contains(sq) ||
                pb.BoatLocations.Submarine.Contains(sq))
            {
                Controls["playerBoard"].Controls[sq].Text = "X";
                lastMessage.Text += "The Bot fired at " + sq + ", it was a hit\n";
                string ans = IsShipDestroyed(pb, sq, false);
                if (ans != "")
                {
                    lastMessage.Text += gs.PlayerName + "'s " + ans + " got destroyed \n";
                    pb.BoatLocations.Destroyed[ans] = true;
                    if (pb.BoatLocations.allShipsDestroyed())
                    {
                        lastMessage.Text += "Game Ended, Bot won!";
                        gs.Losses++;
                        EndGame(false);
                    }
                }
            }
            else
            {
                lastMessage.Text += "The Bot fired at " + sq + ", it was a miss\n";
                Controls["playerBoard"].Controls[sq].Text = "-";
            }
            
        }

        private String IsShipDestroyed(Board board, String sq, bool player)
        {
            List<String> availableSquares;
            if (player)
            {
                availableSquares = availableSquaresPlayer;
            }
            else
            {
                availableSquares = availableSquaresBot;
            }
            String returnString = "";
            BoatLocations bl = board.BoatLocations;
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

        private void EndGame(bool player)
        {
            timer1.Enabled = false;
            GameEndScreen ges = new GameEndScreen(gs, player, false);
            ges.Owner = this;
            ges.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to reset the current game? it will count as a loss.", "Confirm", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                gs.Losses++;
                timer1.Enabled = false;
                GameEndScreen ges = new GameEndScreen(gs, false, false);
                ges.Owner = this;
                ges.Show();

            }
        }

        public void Replay()
        {
            timer1.Enabled = true;
            Controls["playerBoard"].Dispose();
            Controls["botBoard"].Dispose();
            InitializeBoards();
            lastMessage.Text = "";
            messageHistroy.Text = "";
        }

        public void EndSession()
        {
            gs.SaveToDB();
            GameEndScreen ges = new GameEndScreen(gs, false, true);
            ges.Owner = this;
            ges.Show();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            gs.Time++;
            int mins = (gs.Time / 60);
            string timeText = mins + ":" + (gs.Time - (mins * 60));
            label3.Text = "Time: " + timeText;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to end the current session? current game will count as a loss.", "Confirm", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                GameEndScreen ges = new GameEndScreen(gs, false, true);
                ges.Owner = this;
                ges.Show();
            }
        }
    }
}
