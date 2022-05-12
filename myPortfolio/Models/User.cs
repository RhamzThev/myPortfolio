using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Npgsql;

namespace myPortfolio.Models
{
    public class User
    {

        private static User _user;

        private static string _name;

        public static string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private static string _username;

        public static string Username
        {
            get { return _username; }
            set { _username = value; }
        }



        private User(string name, string username)
        {
            _username = username;
            _name = name;
        }



        /*
         * HELPER FUCNTIONS
         */


        private static bool UsernameExists(string username)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = string.Format("SELECT EXISTS(SELECT 1 FROM users WHERE usersUID = '{0}')", username);

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            bool usernameExists = reader.GetBoolean(0);

            if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }

            return usernameExists;
        }

        private static bool ValidCredentials(string username, string password)
        {
            // IF USERNAME EXISTS
            if(UsernameExists(username))
            {
                // IF CORRECT PASSWORD "SELECT EXISTS(SELECT 1 FROM users WHERE usersUID = '{0}' AND usersPWD = '{1}')"
                using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
                conn.Open();

                string cmdText = string.Format("SELECT EXISTS(SELECT 1 FROM users WHERE usersUID = '{0}' AND usersPWD = '{1}')", username, password);

                using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
                using NpgsqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                bool validCredentials = reader.GetBoolean(0);

                if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }

                if (validCredentials)
                {
                    // RETURN TRUE
                    return true;
                }

                // ELSE RETURN FALSE
                return false;
            }


            // ELSE RETURN FALSE
            return false;


        }


        /*
         * CRUD OPERATIONS
         */


        // CREATE (INSERT INTO)
        public static User CreateUser(string name, string username, string password)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = string.Format("INSERT INTO users (usersName, usersUID, usersPWD) VALUES ('{0}', '{1}', '{2}') RETURNING *", name, username, password);

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            object[] values = new object[reader.FieldCount];
            int num = reader.GetValues(values);

            if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }

            User user = new User((string)values[1], (string)values[2]);
            return user;
        }

        // READ (SELECT)
        public static User ReadUserByUsername(string username)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = string.Format("SELECT * FROM users WHERE usersUID = '{0}';", username);

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            object[] values = new object[reader.FieldCount];

            int num = reader.GetValues(values);

            if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }

            User user = new User((string)values[1], (string)values[2]);

            return user;
        }

        // UPDATE (UPDATE)

        // DELETE (DELETE FROM)


        /*
         * GET/SET INSTANCE + BUSINESS LOGIC
         */

        public static User GetInstance()
        {
            if(_user == null)
            {
                _user = new User("Guest", "Guest");
            }

            return _user;
        }

        public static void SetInstance(User user)
        {
            _user = user;
        }

        public static bool LogIn(string username, string password)
        {
            // IF VALID CREDENTIALS
            if (ValidCredentials(username, password))
            {
                // RETURN USER WITH INFO
                _user = ReadUserByUsername(username);
                return true;
            }
            // ELSE RETURN NULL
            return false;
        }

        public static void SignInGuest()
        {
            GetInstance();
        }

    }
}
