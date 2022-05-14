using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class DeleteAccountCommand : BaseCommand
    {
        private readonly Navigation _navigation;

        public DeleteAccountCommand(Navigation navigation)
        {
            _navigation = navigation;
        }

        public override void Execute(object parameter)
        {
            bool deletedAccount = User.DeleteAccount();

            if(deletedAccount)
            {
                _navigation.CurrentViewModel = new StartViewModel(_navigation);
            }
        }
    }
}
