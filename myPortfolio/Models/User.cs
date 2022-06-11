using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Npgsql;
using myPortfolio.Models.Database;

namespace myPortfolio.Models
{
    /// <summary>
    /// Represents a user of the application.
    /// </summary>
    public class User : Database.Database
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

        private static bool _isGuest;

        public static bool IsGuest
        {
            get { return _isGuest; }
            set { _isGuest = value; }
        }

        /// <summary>
        /// Constructor for <c>User</c>.
        /// Assumes user is not a guest.
        /// </summary>
        /// <param name="name">Name of the user.</param>
        /// <param name="username">Username of the user.</param>
        private User(string name, string username)
        {
            _username = username;
            _name = name;
            _isGuest = false;
        }

        /// <summary>
        /// Constructor for <c>User</c>.
        /// </summary>
        /// <param name="name">Name of the user.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="isGuest">Determines if user is a guest.</param>
        private User(string name, string username, bool isGuest)
        {
            _username = username;
            _name = name;
            _isGuest = isGuest;
        }


        /*
         * HELPER FUCNTIONS
         */

        /// <summary>
        /// Determines if the given username exists from databse.
        /// </summary>
        /// <param name="username">Username used to determine if it exists within databse.</param>
        /// <returns><see langword="true"/> if username exists. <see langword="false"/> if otherwise.</returns>
        private static bool UsernameExists(string username)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = string.Format("SELECT EXISTS(SELECT 1 FROM users WHERE username = '{0}')", username);

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            bool usernameExists = reader.GetBoolean(0);

            if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }

            return usernameExists;
        }

        /// <summary>
        /// Determines if given credentials are valid and they exist.
        /// </summary>
        /// <param name="username">Username that possibly exists within database.</param>
        /// <param name="password">Corresponding password to given username.</param>
        /// <returns><see langword="true"/> if credentials are valid. <see langword="false"/> if otherwise.</returns>
        private static bool ValidCredentials(string username, string password)
        {
            // IF USERNAME EXISTS
            if(UsernameExists(username))
            {
                // IF CORRECT PASSWORD "SELECT EXISTS(SELECT 1 FROM users WHERE usersUID = '{0}' AND usersPWD = '{1}')"
                using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
                conn.Open();

                string cmdText = string.Format("SELECT EXISTS(SELECT 1 FROM users WHERE username = '{0}' AND password = '{1}')", username, password);

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
                MessageBox.Show("Incorret password. Please try again.");
                return false;
            }

            // ELSE RETURN FALSE
            MessageBox.Show("Username does not exists. Please try again.");
            return false;


        }

        /*
         * CRUD OPERATIONS
         */

        // CREATE

        /// <summary>
        /// Inserts new user information into database.
        /// </summary>
        /// <param name="name">Name of the user.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="password">Password for created user.</param>
        /// <returns>The newly created user.</returns>
        private static User CreateUser(string name, string username, string password)
        {
            // Open DB
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            // Insert new user information into database
            string cmdText = string.Format("INSERT INTO users (name, username, password) VALUES ('{0}', '{1}', '{2}') RETURNING *", name, username, password);
            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);

            // Retrived inserted user information
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            object[] values = new object[reader.FieldCount];
            int num = reader.GetValues(values);
            if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }

            // Return User object of created user.
            User user = new User((string)values[1], (string)values[3]);
            return user;
        }

        // READ

        /// <summary>
        /// Retrieves user from database based on username.
        /// </summary>
        /// <param name="username">Username that'll be prompted into database.</param>
        /// <returns>a <c>User</c> object if user is retrieved.</returns>
        private static User ReadUserByUsername(string username)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = string.Format("SELECT * FROM users WHERE username = '{0}';", username);

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            object[] values = new object[reader.FieldCount];

            int num = reader.GetValues(values);

            if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }

            User user = new User((string)values[1], (string)values[3]);

            return user;
        }

        // UPDATE (UPDATE)

        /// <summary>
        /// Updates the name of the current user.
        /// </summary>
        /// <param name="name">The updated name.</param>
        /// <returns>The user with the updated information.</returns>
        private static User UpdateUserName(string name)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = string.Format("UPDATE users SET name = '{0}' WHERE username = '{1}' RETURNING *", name, _username);

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            object[] values = new object[reader.FieldCount];
            int num = reader.GetValues(values);

            if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }

            User user = new User((string)values[1], (string)values[3]);
            return user;
        }

        /// <summary>
        /// Updates the password of the current user.
        /// </summary>
        /// <param name="password">The updated password.</param>
        private static void UpdateUserPassword(string password)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = string.Format("UPDATE users SET password = '{0}' WHERE username = '{1}' RETURNING *", password, _username);

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            cmd.ExecuteNonQuery();

            if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }
        }

        // DELETE (DELETE FROM)

        /// <summary>
        /// Deletes current user from database.
        /// </summary>
        private static void DeleteUser()
        {
            using NpgsqlConnection conn = new NpgsqlConnection(Database.Database.connectionString);
            conn.Open();

            string cmdText = string.Format("DELETE FROM users WHERE username = '{0}'", _username);

            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            cmd.ExecuteNonQuery();

            if (conn.State == System.Data.ConnectionState.Open) { conn.Close(); }
        }

        /*
         * BUSINESS LOGIC
         */

        /// <summary>
        /// Logs user into system.
        /// </summary>
        /// <param name="username">The attempted username.</param>
        /// <param name="password">The attempted password.</param>
        /// <returns><see langword="true"/> if User logged in successfully. <see langword="false"/> if otherwise.</returns>
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

        /// <summary>
        /// Signs in user as a guest.
        /// </summary>
        public static void SignInGuest()
        {
            _user = new User("Guest", "Guest", true);
        }

        /// <summary>
        /// Signs user up with a new account.
        /// </summary>
        /// <param name="name">Name of new user.</param>
        /// <param name="username">Username of created user.</param>
        /// <param name="password">Password for new user.</param>
        /// <param name="repeatPassword">A repeated password for the new user.</param>
        /// <returns><see langword="true"/> if the user is signed in correctly. <see langword="false"/> if otherwise.</returns>
        public static bool SignUp(string name, string username, string password, string repeatPassword)
        {
            // IF USERNAME IS NOT TAKEN
            if(!UsernameExists(username))
            {
                // RETURN CREATED USER
                _user = CreateUser(name, username, password);
                return true;
            }

            // ELSE RETURN FALSE
            MessageBox.Show("Username is taken. Please try again.");
            return false;
        }

        /// <summary>
        /// Signs user out of application.
        /// </summary>
        public static void SignOut()
        {
            _user = null;
        }

        /// <summary>
        /// Updates name of current user.
        /// </summary>
        /// <param name="name">The newly updated name</param>
        /// <returns><see langword="true"/> if the name is updated successfully. <see langword="false"/> if otherwise.</returns>
        public static bool UpdateName(string name)
        {
            string messageBox = string.Format("This will update name from {0} to {1}", User.Name, name);
            MessageBoxResult messageBoxResult = MessageBox.Show(messageBox, "Update Name", MessageBoxButton.OKCancel); 

            if (messageBoxResult == MessageBoxResult.OK)
            {
                _user = UpdateUserName(name);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates password of current user.
        /// </summary>
        /// <param name="password">The newly updated password.</param>
        /// <param name="previousPassword">The previous (current) password of the current user.</param>
        /// <returns></returns>
        public static bool UpdatePassword(string password, string previousPassword)
        {
            if(ValidCredentials(_username, previousPassword))
            {
                if(!string.Equals(password, previousPassword))
                {
                    UpdateUserPassword(password);
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// Deletes current user from database.
        /// </summary>
        /// <returns><see langword="true"/> if user is successfully deleted. <see langword="false"/> if otherwise.</returns>
        public static bool DeleteAccount()
        {
            string messageBox = "Are you sure you want to delete your account? This action will be irreversible.";
            MessageBoxResult messageBoxResult = MessageBox.Show(messageBox, "Delete Account", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                DeleteUser();
                return true;
            }
            return false;
        }
    }
}
