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
    /// <summary>
    /// Class <c>LogInCommand</c> inherits <c>BaseCommand</c>. Executes command to log in user into main portion of the application.
    /// </summary>
    public class LogInCommand : BaseCommand
    {

        private readonly StartViewModel _startViewModel;
        private readonly Navigation _navigation;

        /// <summary>
        /// Constructor for <c>LogInCommand</c>. 
        /// </summary>
        /// <param name="startViewModel"></param>
        /// <param name="navigation"></param>
        public LogInCommand(StartViewModel startViewModel, Navigation navigation)
        {
            _startViewModel = startViewModel;
            _startViewModel.PropertyChanged += OnPropertyChanged;

            _navigation = navigation;
        }

        /// <summary>
        /// Defines a method that is called when the properties of TextBoxes have changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            bool loggedIn = User.LogIn(username, password);

            // GO TO MAIN PAGE
            if(loggedIn)
            {
                _navigation.CurrentViewModel = new HomeViewModel(_navigation);
            } 
        }
    }
}
