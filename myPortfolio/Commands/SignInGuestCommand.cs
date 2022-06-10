using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>SignInGuestCommand</c> inherits <c>BaseCommand</c>. 
    /// Executes command to sign in guest user into application.
    /// </summary>
    public class SignInGuestCommand : BaseCommand
    {
        private readonly Navigation _navigation;

        /// <summary>
        /// Constructor for <c>SignInGuestCommand</c>. 
        /// </summary>
        /// <param name="navigation"></param>
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
