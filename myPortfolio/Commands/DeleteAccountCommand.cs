using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>DeleteAccountCommand</c> inherits <c>BaseCommand</c>. Executes command to delete account from database.
    /// </summary>
    public class DeleteAccountCommand : BaseCommand
    {
        private readonly Navigation _navigation;

        /// <summary>
        /// Constructor for <c>DeleteAccountCommand</c>. 
        /// </summary>
        /// <param name="navigation"></param>
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
