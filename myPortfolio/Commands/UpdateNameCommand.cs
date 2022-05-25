using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace myPortfolio.Commands
{
    public class UpdateNameCommand : BaseCommand
    {
        private readonly ProfileViewModel _profileViewModel;

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
