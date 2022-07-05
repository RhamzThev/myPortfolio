using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using myPortfolio.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>UpdatePasswordCommand</c> inherits <c>BaseCommand</c>. 
    /// Executes command to open the confirmation popup to update the password. 
    /// </summary>
    public class UpdatePasswordCommand : BaseCommand
    {

        private ProfileViewModel _profileViewModel;

        /// <summary>
        /// Constructor for <c>UpdatePasswordCommand</c>. 
        /// </summary>
        /// <param name="profileViewModel"></param>
        public UpdatePasswordCommand(ProfileViewModel profileViewModel)
        {
            _profileViewModel = profileViewModel;
        }

        public override void Execute(object parameter)
        {
            _profileViewModel.PopupIsOpen = true;
            //_profileViewModel.IsEnabled = false;
        }
    }
}
