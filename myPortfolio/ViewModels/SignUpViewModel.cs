using myPortfolio.Commands;
using myPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace myPortfolio.ViewModels
{
    /// <summary>
    /// Class <c>SignUpViewModel</c> inherits <c>BaseViewModel</c>. Handles the Data binding for SignUpView.
    /// </summary>
    public class SignUpViewModel : BaseViewModel
    {

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

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

        private string _repeatPassword;

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set { _repeatPassword = value; OnPropertyChanged(nameof(RepeatPassword)); }
        }

        public ICommand BackCommand { get; }
        public ICommand SignUpCommand { get; }

        /// <summary>
        /// Constructor for <c>SignUpViewModel</c>.
        /// </summary>
        /// <param name="navigation">Class used to navigate across various view models.</param>
        public SignUpViewModel(Navigation navigation)
        {
            BackCommand = new NavigateCommand<StartViewModel>(navigation, () => new StartViewModel(navigation));
            SignUpCommand = new SignUpCommand(this, navigation);

        }
    }
}
