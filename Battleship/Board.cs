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
        public Board(bool player, BoatLocations bl)
        {

            if (player) {
                this.Size = new Size(550, 550);

                ToolTip tp = new ToolTip();
                String[] letters = new String[10] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

                int cellSize = 50;
                for (int row = 0; row < 11; row++)
                {
                    for (int col = 0; col < 11; col++)
                    {
                        if ((col == 0) || (row == 0))
                        {
                            Label lbl = new Label();
                            if ((col == 0) && (row > 0))
                            {
                                lbl.Text = letters[row -1];
                            }
                            else if ((row == 0) && (col > 0))
                            {
                                lbl.Text = col.ToString();
                            }

                            lbl.TextAlign = ContentAlignment.MiddleCenter;
                            //lbl.BackColor = Color.Gray;
                            lbl.Location = new Point(col * cellSize, row * cellSize);
                            lbl.Size = new Size(cellSize, cellSize);
                            this.Controls.Add(lbl);
                        }
                        else
                        {
                            Label lbl = new Label();
                            lbl.Name = letters[row - 1] + col;
                            if (bl.AircraftCarrier.Contains(lbl.Name))
                            {
                                lbl.BackColor = Color.GreenYellow;
                                tp.SetToolTip(lbl, "Aircraft Carrier | " + lbl.Name);

                            }
                            else if (bl.Destroyer.Contains(lbl.Name))
                            {
                                lbl.BackColor = Color.Red;
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
                                lbl.BackColor = Color.Aqua;
                                tp.SetToolTip(lbl, lbl.Name);
                            }
                            lbl.BorderStyle = BorderStyle.FixedSingle;
                            
                            lbl.Size = new Size(cellSize, cellSize);
                            lbl.Location = new Point(col * cellSize, row * cellSize);
                            this.Controls.Add(lbl);
                            //Button btn = new Button();
                            //btn.BackColor = Color.Aqua;
                            //btn.FlatStyle = FlatStyle.Flat;
                            //btn.FlatAppearance.BorderSize = 1;
                            //btn.FlatAppearance.BorderColor = SystemColors.ControlDark;
                            //btn.Size = new Size(cellSize, cellSize);
                            //btn.Location = new Point(col * cellSize, row * cellSize);
                            //btn.Text = "";
                            //btn.Name = letters[row - 1] + col;
                            //tp.SetToolTip(btn, btn.Name);
                            //this.Controls.Add(btn);
                        }
                    }
                }
            }

        }

        
    }
}
