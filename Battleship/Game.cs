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
        public Game()
        {
            InitializeComponent();
            InitializeBoards();

            Console.WriteLine(Controls.Count);
        }


        private void InitializeBoards()
        {
            Random rnd = new Random();
            BoatLocations playerBoatLocations = PickLocations(rnd);
            BoatLocations botBoatLocations = PickLocations(rnd);


            Board playerBoard = new Board(true, playerBoatLocations);
            playerBoard.Name = "playerBoard";
            Board botBoard = new Board(false, botBoatLocations);
            botBoard.Name = "botBoard";

            playerBoard.Location = new Point(40, 40);
            botBoard.Location = new Point(650, 40);

            this.Controls.Add(playerBoard);
            this.Controls.Add(botBoard);
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
    }
}
