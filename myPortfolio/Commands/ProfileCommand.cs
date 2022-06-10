using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>ProfileCommand</c> inherits <c>BaseCommand</c>. Sets the navigator's current view model to the profile view model.
    /// </summary>
    public class ProfileCommand : BaseCommand
    {
        private Navigation _navigation;

        /// <summary>
        /// Constructor for <c>ProfileCommand</c>. 
        /// </summary>
        /// <param name="navigation"></param>
        public ProfileCommand(Navigation navigation)
        {
            _navigation = navigation;
        }

        public override bool CanExecute(object parameter)
        {
            return !User.IsGuest && base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = new ProfileViewModel(_navigation);
        }
    }
}
