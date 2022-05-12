﻿using myPortfolio.Commands;
using myPortfolio.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace myPortfolio.ViewModels
{
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

        public StartViewModel(Navigation navigation)
        {
            LogInCommand = new LogInCommand(this, navigation);
        }

    }
}
