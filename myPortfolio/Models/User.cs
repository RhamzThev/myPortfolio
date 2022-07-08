using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Data.SqlClient;
using Npgsql;
using myPortfolio.Models.Database;
using System.Data;
using System.Linq;

namespace myPortfolio.Models
{
    /// <summary>
    /// Represents a user of the application.
    /// </summary>
    public class User : Database.Database
    {

        private static readonly int NAME_INDEX = 1;
        private static readonly int PASSWORD_INDEX = 2;
        private static readonly int USERNAME_INDEX = 3;

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

        public User()
        {

        }

        /*
         * HELPER FUCNTIONS
         */

        /// <summary>
        /// Determines if the given username exists from databse.
        /// </summary>
        /// <param name="username">Username used to determine if it exists within databse.</param>
        /// <returns><see langword="true"/> if username exists. <see langword="false"/> if otherwise.</returns>
        private static void UsernameExists(string username, Action<bool> completionHandler)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Database.Database.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = @"IF EXISTS(SELECT 1 FROM users WHERE username = @username)
                        BEGIN SELECT CAST(1 AS BIT) END
                        ELSE
                        BEGIN SELECT CAST(0 AS BIT) END";

                    SqlParameter usernameParam = new SqlParameter("@username", System.Data.SqlDbType.VarChar, 255);
                    usernameParam.Value = username;
                    command.Parameters.Add(usernameParam);

                    command.Prepare();
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();

                    Console.WriteLine("Hello");

                    completionHandler(reader.GetBoolean(0));
                }
            } catch (Exception e)
            {
                completionHandler(false);
            }
        }

        /// <summary>
        /// Determines if given credentials are valid and they exist.
        /// </summary>
        /// <param name="username">Username that possibly exists within database.</param>
        /// <param name="password">Corresponding password to given username.</param>
        /// <returns><see langword="true"/> if credentials are valid. <see langword="false"/> if otherwise.</returns>
        private static void ValidCredentials(string username, string password, Action<bool> completionHandler)
        {

            //IF USERNAME EXISTS
            UsernameExists(username, (bool userExists) =>
            {
                if(userExists)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(Database.Database.connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(null, connection);

                            command.CommandText = @"IF EXISTS(SELECT 1 FROM users WHERE username = @username AND password = @password)
                                BEGIN SELECT CAST(1 AS BIT) END
                                ELSE
                                BEGIN SELECT CAST(0 AS BIT) END";

                            SqlParameter usernameParam = new SqlParameter("@username", System.Data.SqlDbType.VarChar, 255);
                            SqlParameter passwordParam = new SqlParameter("@password", System.Data.SqlDbType.VarChar, 255);

                            usernameParam.Value = username;
                            passwordParam.Value = password;

                            command.Parameters.Add(usernameParam);
                            command.Parameters.Add(passwordParam);

                            command.Prepare();
                            SqlDataReader reader = command.ExecuteReader();

                            reader.Read();

                            completionHandler(reader.GetBoolean(0));
                        }
                    }
                    catch (Exception e)
                    {
                        completionHandler(false);
                    }
                } else
                {
                    completionHandler(false);
                }
            });
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
        private static void CreateUser(string name, string username, string password, Action<User> completionHandler)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(Database.Database.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = "INSERT INTO users (name, username, password) OUTPUT INSERTED.* VALUES (@name, @username, @password)";

                    SqlParameter nameParam = new SqlParameter("@name", System.Data.SqlDbType.VarChar, 255);
                    SqlParameter usernameParam = new SqlParameter("@username", System.Data.SqlDbType.VarChar, 255);
                    SqlParameter passwordParam = new SqlParameter("@password", System.Data.SqlDbType.VarChar, 255);
                    
                    nameParam.Value = name;
                    usernameParam.Value = username;
                    passwordParam.Value = password;

                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(passwordParam);

                    command.Prepare();
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    object[] values = new object[reader.FieldCount];
                    int num = reader.GetValues(values);

                    string readerName = values.ElementAtOrDefault(NAME_INDEX).ToString();
                    string readerUsername = values.ElementAtOrDefault(USERNAME_INDEX).ToString();

                    completionHandler(new User(readerName, readerUsername));

                }
            }
            catch (Exception e)
            {
                //SOME CUSTOM EXCEPTION
                throw;
            }
        }

        // READ

        /// <summary>
        /// Retrieves user from database based on username.
        /// </summary>
        /// <param name="username">Username that'll be prompted into database.</param>
        /// <returns>a <c>User</c> object if user is retrieved.</returns>
        private static void ReadUserByUsername(string username, Action<User> completionHandler)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = "SELECT * FROM users WHERE username = @username;";

                    SqlParameter usernameParam = new SqlParameter("@username", System.Data.SqlDbType.VarChar, 255);

                    usernameParam.Value = username;

                    command.Parameters.Add(usernameParam);

                    command.Prepare();
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    object[] values = new object[reader.FieldCount];
                    int num = reader.GetValues(values);

                    string readerName = values.ElementAtOrDefault(NAME_INDEX).ToString();
                    string readerUsername = values.ElementAtOrDefault(USERNAME_INDEX).ToString();

                    completionHandler(new User(readerName, readerUsername));

                }
            }
            catch (Exception e)
            {
                // SOME CUSTOM EX
                throw;
            }
        }

        // UPDATE (UPDATE)

        /// <summary>
        /// Updates the name of the current user.
        /// </summary>
        /// <param name="name">The updated name.</param>
        /// <returns>The user with the updated information.</returns>
        private static void UpdateUserName(string name, Action<User> completionHandler)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Database.Database.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = @"UPDATE users SET name = @name OUTPUT INSERTED.* WHERE username = @username";

                    SqlParameter nameParam = new SqlParameter("@name", SqlDbType.VarChar, 255);
                    SqlParameter usernameParam = new SqlParameter("@username", SqlDbType.VarChar, 255);

                    nameParam.Value = name;
                    usernameParam.Value = _username;

                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(usernameParam);

                    command.Prepare();
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    object[] values = new object[reader.FieldCount];
                    int num = reader.GetValues(values);

                    string readerName = values.ElementAtOrDefault(NAME_INDEX).ToString();
                    string readerUsername = values.ElementAtOrDefault(USERNAME_INDEX).ToString();

                    completionHandler(new User(readerName, readerUsername));

                }
            }
            catch (Exception e)
            {
                // SOME CUSTOM EX
                throw;
            }
        }

        /// <summary>
        /// Updates the password of the current user.
        /// </summary>
        /// <param name="password">The updated password.</param>
        private static void UpdateUserPassword(string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Database.Database.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = "UPDATE users SET password = @password OUTPUT INSERTED.* WHERE username = @username";

                    SqlParameter passwordParam = new SqlParameter("@password", SqlDbType.VarChar, 255);
                    SqlParameter usernameParam = new SqlParameter("@username", SqlDbType.VarChar, 255);

                    usernameParam.Value = _username;
                    passwordParam.Value = password;

                    command.Parameters.Add(usernameParam);
                    command.Parameters.Add(passwordParam);

                    command.Prepare();
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                //SOME CUSTOM EX
                throw;
            }
        }

        // DELETE (DELETE FROM)

        /// <summary>
        /// Deletes current user from database.
        /// </summary>
        private static void DeleteUser()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Database.Database.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(null, connection);

                    command.CommandText = "DELETE FROM users WHERE username = @username"; 

                    SqlParameter usernameParam = new SqlParameter("@username", System.Data.SqlDbType.VarChar, 255);

                    usernameParam.Value = _username;

                    command.Parameters.Add(usernameParam);

                    command.Prepare();

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {
                //SOME EX
                throw;
            }

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
        public static void LogIn(string username, string password, Action<bool> completionHandler)
        {
            // IF VALID CREDENTIALS
            ValidCredentials(username, password, (bool isLoggedInState) => {

                if (isLoggedInState)
                {
                    // RETURN USER WITH INFO
                    ReadUserByUsername(username, (User user) =>
                    {
                        _user = user;
                        completionHandler(true);
                    });

                    // ELSE RETURN FALSE
                } else { completionHandler(false); }
            });
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
        public static void SignUp(string name, string username, string password, string repeatPassword, Action<bool> completionHandler)
        {
            // IF USERNAME IS NOT TAKEN
            UsernameExists(username, (bool accountExists) =>
            {
                if (!accountExists)
                {
                    // RETURN CREATED USER
                    CreateUser(name, username, password, (User user) =>
                    {
                        _user = user;
                        completionHandler(true);
                    });
                }
                else
                {
                    // ELSE RETURN FALSE
                    MessageBox.Show("Username is taken. Please try again.");
                    completionHandler(false);
                }
            });
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
        public static void UpdateName(string name, Action<bool> completionHandler)
        {
            string messageBox = string.Format("This will update name from {0} to {1}", User.Name, name);
            MessageBoxResult messageBoxResult = MessageBox.Show(messageBox, "Update Name", MessageBoxButton.OKCancel); 

            if (messageBoxResult == MessageBoxResult.OK)
            {
                UpdateUserName(name, (User user) =>
                {
                    _user = user;
                    completionHandler(true);
                });
            } else
            {
                completionHandler(false);
            }
        }

        /// <summary>
        /// Updates password of current user.
        /// </summary>
        /// <param name="password">The newly updated password.</param>
        /// <param name="previousPassword">The previous (current) password of the current user.</param>
        /// <returns></returns>
        public static void UpdatePassword(string password, string previousPassword, Action<bool> completionHandler)
        {
            ValidCredentials(_username, previousPassword, (bool isValid) =>
            {
                if (isValid && !string.Equals(password, previousPassword))
                {
                    UpdateUserPassword(password);
                    completionHandler(true);
                }
                else
                {
                    completionHandler(false);
                }

            });
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
