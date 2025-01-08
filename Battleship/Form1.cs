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
    public partial class Form1 : Form
    {
        Color[,] colors = new Color[10, 10];
        public Form1()
        {
            InitializeComponent();
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            // initialize colors 
            ResetColors(colors);
            foreach (Label l in tableLayoutPanel1.Controls.OfType<Label>())
            {
                l.Click += new System.EventHandler(this.label1_Click);

            }
        }

        private void ResetColors(Color[,] colors)
        {
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    colors[i, j] = SystemColors.Control;
                }
            }
        }

        Point GetPointFromCoords(TableLayoutPanel table, Point point)
        {
            Point pos = new Point();
            pos.X = (point.X / 50);
            pos.Y = (point.Y / 50);

            return pos;
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            using (Brush brush = new SolidBrush(colors[e.Column, e.Row]))
            {
                e.Graphics.FillRectangle(brush, e.CellBounds);
            }
        }

        //private void tableLayoutPanel1_Click(object sender, EventArgs e)
        //{
        //    ResetColors(colors);
        //    Point cellPosition =
        //        GetPointFromCoords(tableLayoutPanel1, tableLayoutPanel1.PointToClient(Cursor.Position));
        //    colors[cellPosition.X, cellPosition.Y] = Color.Red;
        //    tableLayoutPanel1.Refresh();
        //}

        private void label1_Click(object sender, EventArgs e)
        {
            Point cellPosition =
                GetPointFromCoords(tableLayoutPanel1, tableLayoutPanel1.PointToClient(Cursor.Position));
            label101.Text = cellPosition.X.ToString() + " " + cellPosition.Y.ToString();
        }
    }
}
