using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace myPortfolio.Models
{
    public class App
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

        public App(string name, string description, string folder, string exePath)
        {
            _name = name;
            _description = description;
            _folder = folder;
            _exePath = exePath;
        }

        public static List<App> Apps
        {
            get { return ReadApps(); }
        }


        /*
         * HELPER FUNCTIONS
         */


        /*
         * CRUD OPERATIONS
         */

        private static List<App> ReadApps()
        {

            // GET GAMES
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = "SELECT * FROM apps";

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            List<App> apps = new List<App>();

            while (reader.Read())
            {

                string name = reader.GetString(1);
                string description = reader.GetString(2);
                string folder = reader.GetString(3);
                string exePath = reader.GetString(4);

                apps.Add(new App(name, description, folder, exePath));
            }

            return apps;

        }

        /*
         * BUSINESS LOGIC
         */

        public void ExecuteApp()
        {
            string filename = string.Format("../Apps/{0}{1}", _description, _exePath);
            Process.Start(filename);
        }
    }
}
