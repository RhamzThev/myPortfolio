using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    class SignOutCommand : BaseCommand
    {
        private Navigation _navigation;

        public SignOutCommand(Navigation navigation)
        {
            _navigation = navigation;
        }

        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = new StartViewModel(_navigation);

            User.SetInstance(null);
        }
    }
}
