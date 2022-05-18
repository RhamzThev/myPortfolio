using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Models
{
    public class Game
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _folder;
        private string _exePath;

        public Game(string name, string description, string folder, string exePath)
        {
            _name = name;
            _description = description;
            _folder = folder;
            _exePath = exePath;
        }

        public static List<Game> Games
        {
            get { return ReadGames(); }
        }


        /*
         * HELPER FUNCTIONS
         */


        /*
         * CRUD OPERATIONS
         */

        private static List<Game> ReadGames()
        {

            // GET GAMES
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = "SELECT * FROM games";

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            List<Game> games = new List<Game>();

            while (reader.Read())
            {

                string name = reader.GetString(1);
                string description = reader.GetString(2);
                string folder = reader.GetString(3);
                string exePath = reader.GetString(4);

                games.Add(new Game(name, description, folder, exePath));
            }

            return games;

        }

        /*
         * BUSINESS LOGIC
         */

    }
}
