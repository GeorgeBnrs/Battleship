using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
            if (!File.Exists("gamehistory.sqlite"))
            {
                dataGridView1.Visible = false;
                label1.Visible = true;
            }
            else
            {
                List<GameStats> allStats = new List<GameStats>();
                SQLiteConnection dbConnection;
                string SQLString;
                SQLiteCommand command;
                dbConnection = new SQLiteConnection("Data Source=gamehistory.sqlite;Version=3;");
                dbConnection.Open();
                SQLString = "select * from Games";
                command = new SQLiteCommand(SQLString, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GameStats gs = new GameStats();
                    gs.PlayerName = reader["Player"].ToString();
                    gs.Time =  (int)reader["Time"];
                    gs.Wins = (int)reader["Wins"];
                    gs.Losses = (int)reader["Losses"];
                    allStats.Add(gs);
                }
                dbConnection.Close();
                //dataGridView1.DataSource = allStats;
                dataGridView1.Font = new Font(dataGridView1.Font.Name, 14.5f);
                dataGridView1.Columns.Add("Player", "Player");
                dataGridView1.Columns.Add("Time", "Time");
                dataGridView1.Columns.Add("Wins", "Wins");
                dataGridView1.Columns.Add("Losses", "Losses");

                foreach (GameStats gs in allStats)
                {
                    dataGridView1.Rows.Add(gs.PlayerName, gs.TimeFormatted(), gs.Wins, gs.Losses);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
