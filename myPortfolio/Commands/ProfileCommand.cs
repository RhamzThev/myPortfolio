using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class ProfileCommand : BaseCommand
    {
        private Navigation _navigation;

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
