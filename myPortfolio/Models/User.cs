using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Npgsql;

namespace myPortfolio.Models
{
    class User
    {

        private string Username { get; }
        private string Name { get; }

        private User(string name, string username)
        {
            Username = username;
            Name = name;
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
         * BUSINESS LOGIC
         */


        public static User LogIn(string username, string password)
        {
            // IF VALID CREDENTIALS
            if (ValidCredentials(username, password))
            {
                // RETURN USER WITH INFO
                return ReadUserByUsername(username);
            }
            // ELSE RETURN NULL
            return null;
        }

    }
}
