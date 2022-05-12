using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class SignInGuestCommand : BaseCommand
    {
        private readonly Navigation _navigation;

        public SignInGuestCommand(Navigation navigation)
        {
            _navigation = navigation;
        }

        public override void Execute(object parameter)
        {
            User.SignInGuest();

            _navigation.CurrentViewModel = new HomeViewModel(_navigation);
        }
    }
}
