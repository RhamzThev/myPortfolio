using myPortfolio.Commands;
using myPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace myPortfolio.ViewModels
{
    /// <summary>
    /// Class <c>StartViewModel</c> inherits <c>BaseViewModel</c>. Handles the Data binding for StartView.
    /// </summary>
    public class StartViewModel : BaseViewModel
    {
        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand LogInCommand { get; }
        public ICommand SignInGuestCommand { get; }
        public ICommand SignUpCommand { get; }

        /// <summary>
        /// Constructor for <c>StartViewModel</c>.
        /// </summary>
        /// <param name="navigation">Class used to navigate across various view models.</param>
        public StartViewModel(Navigation navigation)
        {
            LogInCommand = new LogInCommand(this, navigation);
            SignInGuestCommand = new SignInGuestCommand(navigation);
            SignUpCommand = new NavigateCommand<SignUpViewModel>(navigation , () => new SignUpViewModel(navigation));
        }

    }
}
