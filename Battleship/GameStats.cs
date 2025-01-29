using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;


namespace Battleship
{
    public class GameStats
    {
        public string PlayerName { get; set; }
        public int Time { get; set; } = 0;
        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;

        public string TimeFormatted()
        {
            string time;
            int mins = (Time / 60);
            time = mins + ":" + (Time - (mins * 60));
            return time;
        }
        public void SaveToDB()
        {
            SQLiteConnection dbConnection;
            string SQLString;
            SQLiteCommand command;
            if (!File.Exists("gamehistory.sqlite"))
            {
                Console.WriteLine("Created new Database");
                SQLiteConnection.CreateFile("gamehistory.sqlite");
                dbConnection = new SQLiteConnection("Data Source=gamehistory.sqlite;Version=3;");
                dbConnection.Open();
                SQLString = "create table Games (Player varchar(20), Time int, Wins int, Losses int)";
                command = new SQLiteCommand(SQLString, dbConnection);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
            dbConnection = new SQLiteConnection("Data Source=gamehistory.sqlite;Version=3;");
            dbConnection.Open();
            SQLString = "insert into Games (Player, Time, Wins, Losses) values ('" + PlayerName + "'," + Time +
                               "," + Wins + "," + Losses + ")";
            command = new SQLiteCommand(SQLString, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
    }

    
}
