using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public class Board : Panel
    {
        public BoatLocations BoatLocations { get; set; }

        public Board(bool player, BoatLocations bl)
        {
            this.BoatLocations = bl;
            ToolTip tp = new ToolTip();
            String[] letters = new String[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            int cellSize = 50;
            this.Size = new Size(550, 550);

            if (player)
            {
                for (int row = 0; row < 11; row++)
                {
                    for (int col = 0; col < 11; col++)
                    {
                        if ((col == 0) || (row == 0))
                        {
                            Label lbl = new Label();
                            if ((col == 0) && (row > 0))
                            {
                                lbl.Text = letters[row - 1];
                            }
                            else if ((row == 0) && (col > 0))
                            {
                                lbl.Text = col.ToString();
                            }

                            lbl.TextAlign = ContentAlignment.MiddleCenter;
                            lbl.Location = new Point(col * cellSize, row * cellSize);
                            lbl.Size = new Size(cellSize, cellSize);
                            this.Controls.Add(lbl);
                        }
                        else
                        {
                            Label lbl = new Label();
                            lbl.Name = letters[row - 1] + col;
                            lbl.ForeColor = Color.Red;
                            lbl.Font = new Font(lbl.Font.Name, 14.5f);
                            lbl.TextAlign = ContentAlignment.MiddleCenter;
                            if (bl.AircraftCarrier.Contains(lbl.Name))
                            {
                                lbl.BackColor = Color.GreenYellow;
                                tp.SetToolTip(lbl, "Aircraft Carrier | " + lbl.Name);

                            }
                            else if (bl.Destroyer.Contains(lbl.Name))
                            {
                                lbl.BackColor = Color.MediumPurple;
                                tp.SetToolTip(lbl, "Destroyer | " + lbl.Name);
                            }
                            else if (bl.Warship.Contains(lbl.Name))
                            {
                                lbl.BackColor = Color.Green;
                                tp.SetToolTip(lbl, "Warship | " + lbl.Name);
                            }
                            else if (bl.Submarine.Contains(lbl.Name))
                            {
                                lbl.BackColor = Color.DarkBlue;
                                tp.SetToolTip(lbl, "Submarine | " + lbl.Name);
                            }
                            else
                            {
                                lbl.ForeColor = Color.Black;
                                lbl.BackColor = Color.Aqua;
                                tp.SetToolTip(lbl, lbl.Name);
                            }
                            
                            lbl.BorderStyle = BorderStyle.FixedSingle;
                            
                            lbl.Size = new Size(cellSize, cellSize);
                            lbl.Location = new Point(col * cellSize, row * cellSize);
                            this.Controls.Add(lbl);
                        }
                    }
                }
            }
            else
            {
                for (int row = 0; row < 11; row++)
                {
                    for (int col = 0; col < 11; col++)
                    {
                        if ((col == 0) || (row == 0))
                        {
                            Label lbl = new Label();
                            if ((col == 0) && (row > 0))
                            {
                                lbl.Text = letters[row - 1];
                            }
                            else if ((row == 0) && (col > 0))
                            {
                                lbl.Text = col.ToString();
                            }

                            lbl.TextAlign = ContentAlignment.MiddleCenter;
                            lbl.Location = new Point(col * cellSize, row * cellSize);
                            lbl.Size = new Size(cellSize, cellSize);
                            this.Controls.Add(lbl);
                        }
                        else
                        {
                            Button btn = new Button();
                            btn.Font = new Font(btn.Font.Name, 14.5f);
                            btn.BackColor = Color.Aqua;
                            btn.Name = letters[row - 1] + col;
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.FlatAppearance.BorderColor = Color.DimGray;
                            btn.Text = "";
                            btn.FlatAppearance.BorderSize = 1;
                            btn.Size = new Size(cellSize, cellSize);
                            btn.Location = new Point(col * cellSize, row * cellSize);
                            tp.SetToolTip(btn, btn.Name);
                            if (bl.AircraftCarrier.Contains(btn.Name) || bl.Destroyer.Contains(btn.Name) || bl.Warship.Contains(btn.Name) || bl.Submarine.Contains(btn.Name))
                            {
                                btn.ForeColor = Color.Red;
                                btn.Click += HitClick;

                            }
                            else 
                            {
                                btn.ForeColor = Color.Green;
                                btn.Click += MissClick;
                            }
                            this.Controls.Add(btn);

                        }
                    }
                }
            }
        }

        private void MissClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Text = "-";

            Game g = (Game)this.Parent;
            g.PlayerAction("miss", btn.Name);
        }

        private void HitClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Text = "X";

            Game g = (Game)this.Parent;
            g.PlayerAction("hit", btn.Name);
        }

    }
}
