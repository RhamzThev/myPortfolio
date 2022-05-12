using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace myPortfolio.Commands
{
    public class LogInCommand : BaseCommand
    {

        private readonly StartViewModel _startViewModel;
        private readonly Navigation _navigation;

        public LogInCommand(StartViewModel startViewModel, Navigation navigation)
        {
            _startViewModel = startViewModel;
            _startViewModel.PropertyChanged += OnPropertyChanged;

            _navigation = navigation;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(StartViewModel.Username) || e.PropertyName == nameof(StartViewModel.Password))
            {
                OnCanExecutedChanged();
            }
        }
        public override bool CanExecute(object parameter)
        {
            string username = _startViewModel.Username;
            string password = _startViewModel.Password;

            return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            string username = _startViewModel.Username;
            string password = _startViewModel.Password;

            // LOG IN THE USER
            User user = User.LogIn(username, password);

            // START A SESSION

            // GO TO MAIN PAGE
            _navigation.CurrentViewModel = new HomeViewModel(_navigation);
        }
    }
}
