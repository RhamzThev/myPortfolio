using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>UpdateNameCommand</c> inherits <c>BaseCommand</c>. 
    /// Executes command to update the user's name. 
    /// </summary>
    public class UpdateNameCommand : BaseCommand
    {
        private readonly ProfileViewModel _profileViewModel;

        /// <summary>
        /// Constructor for <c>UpdateNameCommand</c>. 
        /// </summary>
        /// <param name="profileViewModel"></param>
        public UpdateNameCommand(ProfileViewModel profileViewModel)
        {
            _profileViewModel = profileViewModel;
        }

        public override void Execute(object parameter)
        {

            string name = _profileViewModel.Name;

            bool updatedName = User.UpdateName(name);

            _profileViewModel.DisplayName = String.Format("Change Name ({0})", User.Name);
            _profileViewModel.Name = "";

        }
    }
}
