using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace myPortfolio.Models
{
    /// <summary>
    /// Class <c>App</c> is an object representing an external application within the database.
    /// </summary>
    public class App
    {

        private static readonly int NAME_INDEX = 1;
        private static readonly int DESC_INDEX = 4;
        private static readonly int FOLDER_INDEX = 2;
        private static readonly int EXE_INDEX = 3;

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

        /// <summary>
        /// Constructor for class <c>App</c>.
        /// </summary>
        /// <param name="name">Name of the application.</param>
        /// <param name="description">Description of application.</param>
        /// <param name="folder">Name of the folder.</param>
        /// <param name="exePath">Path of the ".exe" file within the folder.</param>
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
         * CRUD OPERATIONS
         */

        /// <summary>
        /// Creates an object of the application.
        /// Adds information regarding created app into the database.
        /// </summary>
        /// <param name="name">Name of the application.</param>
        /// <param name="description">Description of application.</param>
        /// <param name="folder">Name of the folder.</param>
        /// <param name="exePath">Path of the ".exe" file within the folder.</param>
        /// <returns>An object of the created app.</returns>
        private static void CreateApp(string name, string description, string folder, string exePath, Action<App> completionHandler)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(Database.Database.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = "INSERT INTO apps (name, description, folder, exe_path) OUTPUT INSERTED.* VALUES (@name, @description, @folder, @exe_path)";

                    SqlParameter nameParam = new SqlParameter("@name", System.Data.SqlDbType.VarChar, 255);
                    SqlParameter descriptionParam = new SqlParameter("@description", System.Data.SqlDbType.VarChar, 255);
                    SqlParameter folderParam = new SqlParameter("@folder", System.Data.SqlDbType.VarChar, 255);
                    SqlParameter exePathParam = new SqlParameter("@exe_path", System.Data.SqlDbType.VarChar, 255);

                    nameParam.Value = name;
                    descriptionParam.Value = description;
                    folderParam.Value = folder;
                    exePathParam.Value = exePath;

                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(descriptionParam);
                    command.Parameters.Add(folderParam);
                    command.Parameters.Add(exePathParam);

                    command.Prepare();
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    object[] values = new object[reader.FieldCount];
                    int num = reader.GetValues(values);

                    string readerName = values.ElementAtOrDefault(NAME_INDEX).ToString();
                    string readerDescription = values.ElementAtOrDefault(DESC_INDEX).ToString();
                    string readerFolder = values.ElementAtOrDefault(FOLDER_INDEX).ToString();
                    string readerExePath = values.ElementAtOrDefault(EXE_INDEX).ToString();

                    completionHandler(new App(readerName, readerDescription, readerFolder, readerExePath));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Retrieves information regarding apps from the databse.
        /// </summary>
        /// <returns>List of apps within database.</returns>
        private static List<App> ReadApps()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(Database.Database.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = "SELECT * FROM apps";

                    command.Prepare();
                    SqlDataReader reader = command.ExecuteReader();

                    List<App> apps = new List<App>();

                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        int num = reader.GetValues(values);

                        string readerName = values.ElementAtOrDefault(NAME_INDEX).ToString();
                        string readerDescription = values.ElementAtOrDefault(DESC_INDEX).ToString();
                        string readerFolder = values.ElementAtOrDefault(FOLDER_INDEX).ToString();
                        string readerExePath = values.ElementAtOrDefault(EXE_INDEX).ToString();

                        apps.Add(new App(readerName, readerDescription, readerFolder, readerExePath));
                    }

                    return apps;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /*
         * BUSINESS LOGIC
         */

        /// <summary>
        /// Executes application.
        /// </summary>
        public void ExecuteApp()
        {
            //string filename = string.Format("../Apps/{0}{1}", _folder, _exePath);

            string filename = Path.Combine(_appFolder, _folder, _exePath);

            Process.Start(filename);
        }

        /// <summary>
        /// Copies files from given source path to the given target path.
        /// Target path directory will be created upon calling this method.
        /// </summary>
        /// <param name="sourcePath">Path of source files.</param>
        /// <param name="targetPath">Name of target destination for copied files.</param>
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

        /// <summary>
        /// Adds created app into database.
        /// </summary>
        /// <param name="name">Name of the application.</param>
        /// <param name="description">Description of application.</param>
        /// <param name="folder">Name of the folder.</param>
        /// <param name="exePath">Path of the ".exe" file within the folder.</param>
        /// <returns><see langword="true"/> if creation of the app has been successful.
        /// <see langword="false"/> if creation of the app has been unsuccessful.</returns>
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

                CreateApp(name, description, name, exePath, (App app) => {});

                return true;
            }

            // ELSE RETURN FALSE
            return false;

        }
    }
}
