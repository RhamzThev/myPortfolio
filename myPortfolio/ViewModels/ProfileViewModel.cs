using myPortfolio.Models;
using myPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace myPortfolio.ViewModels
{
    public class ProfileViewModel : BaseViewModel
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
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand DeleteAccount { get; } 

        public ProfileViewModel(Navigation navigation)
        {
            _name = User.Name;
            _username = User.Username;
        }
    }
}
