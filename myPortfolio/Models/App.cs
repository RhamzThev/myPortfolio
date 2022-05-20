using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

        private static readonly string _appFolder = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Apps");

        /*
         * HELPER FUNCTIONS
         */


        /*
         * CRUD OPERATIONS
         */

        private static App CreateApp(string name, string description, string folder, string exePath)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = string.Format("INSERT INTO apps (name, description, folder, exe_path) VALUES ('{0}', '{1}', '{2}', '{3}') RETURNING *", name, description, folder, exePath);

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            object[] values = new object[reader.FieldCount];
            int num = reader.GetValues(values);

            if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }

            App app = new App((string)values[1], (string)values[2], (string)values[3], (string)values[4]);
            return app;
        }

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
            //string filename = string.Format("../Apps/{0}{1}", _folder, _exePath);

            string filename = Path.Combine(_appFolder, _folder, _exePath);

            Process.Start(filename);
        }

        private static void CopyFiles(string sourcePath, string targetPath)
        {

            // Create Directory
            Directory.CreateDirectory(targetPath);

            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        public static bool AddApp(string name, string description, string sourceFolder, string sourceExePath)
        {
            // IF STRING IS VALID PATH

            string pattern = "[\\/\\?%\\*:\\|\" <>]";

            if (!Regex.IsMatch(name, pattern))
            {
                // CREATE APP DIRECTORY NAME
                string targetDirectory = Path.Combine(_appFolder, name);

                // TRANSFER ALL FILES TO TARGET DIRECTORY
                CopyFiles(sourceFolder, targetDirectory);

                // CREATE AND ADD GAME TO GAMES IN C# AND POSTGRES
                string exePath = sourceExePath.Substring(sourceFolder.Length + 1);

                App app = CreateApp(name, description, name, exePath);

                return true;
            }

            // ELSE RETURN FALSE
            return false;

        }
    }
}
