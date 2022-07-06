using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>UpdatePasswordConfirmationCommand</c> inherits <c>BaseCommand</c>. 
    /// Executes command for the confirmation to update the password, 
    /// therefore updating passowrd.
    /// </summary>
    public class UpdatePasswordConfirmationCommand : BaseCommand
    {
        private ProfileViewModel _profileViewModel;
        private Navigation _navigation;

        /// <summary>
        /// Constructor for <c>UpdatePasswordConfirmationCommand</c>. 
        /// </summary>
        /// <param name="profileViewModel"></param>
        /// <param name="navigation"></param>
        public UpdatePasswordConfirmationCommand(ProfileViewModel profileViewModel, Navigation navigation)
        {
            _profileViewModel = profileViewModel;
            _navigation = navigation;
        }

        public override void Execute(object parameter)
        {
            string password = _profileViewModel.Password;
            string previousPassword = _profileViewModel.PreviousPassword;

            _profileViewModel.PopupIsOpen = false;

            User.UpdatePassword(password, previousPassword, (bool updatedPassword) =>
            {
                if(updatedPassword)
                {
                    _profileViewModel.Password = String.Empty;
                    _profileViewModel.PreviousPassword = String.Empty;
                }
            });

        }
    }
}
