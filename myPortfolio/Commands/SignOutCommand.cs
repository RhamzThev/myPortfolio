using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>SignOutCommand</c> inherits <c>BaseCommand</c>. 
    /// Executes command to sign the user out of the application. 
    /// </summary>
    class SignOutCommand : BaseCommand
    {
        private readonly Navigation _navigation;

        /// <summary>
        /// Constructor for <c>SignOutCommand</c>. 
        /// </summary>
        /// <param name="navigation"></param>
        public SignOutCommand(Navigation navigation)
        {
            _navigation = navigation;
        }

        public override void Execute(object parameter)
        {
            User.SignOut();
            _navigation.CurrentViewModel = new StartViewModel(_navigation);
        }
    }
}
