using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace myPortfolio.Commands
{
    public class SignUpCommand : BaseCommand
    {
        private readonly SignUpViewModel _signUpViewModel;
        private readonly Navigation _navigation;

        public SignUpCommand(SignUpViewModel signUpViewModel, Navigation navigation)
        {
            _signUpViewModel = signUpViewModel;
            _signUpViewModel.PropertyChanged += OnPropertyChanged;
            
            _navigation = navigation;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SignUpViewModel.Name) ||
                e.PropertyName == nameof(SignUpViewModel.Username) ||
                e.PropertyName == nameof(SignUpViewModel.Password) ||
                e.PropertyName == nameof(SignUpViewModel.RepeatPassword))
            {
                OnCanExecutedChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            string name = _signUpViewModel.Name;
            string username = _signUpViewModel.Username;
            string password = _signUpViewModel.Password;
            string repeatPassword = _signUpViewModel.RepeatPassword;

            return !string.IsNullOrEmpty(name) &&
                !string.IsNullOrEmpty(username) &&
                !string.IsNullOrEmpty(password) &&
                !string.IsNullOrEmpty(repeatPassword) &&
                string.Equals(password, repeatPassword) &&
                base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            string name = _signUpViewModel.Name;
            string username = _signUpViewModel.Username;
            string password = _signUpViewModel.Password;
            string repeatPassword = _signUpViewModel.RepeatPassword;

            // SIGN UP USER
            bool signedUp = User.SignUp(name, username, password, repeatPassword);

            // GO TO MAIN PAGE
            if (signedUp)
            {
                _navigation.CurrentViewModel = new HomeViewModel(_navigation);
            }
        }
    }
}
