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
        private readonly Navigation _navigation;

        public UpdateNameCommand(ProfileViewModel profileViewModel, Navigation navigation)
        {
            _profileViewModel = profileViewModel;
            _navigation = navigation;
        }

        public override void Execute(object parameter)
        {

            string name = _profileViewModel.Name;

            bool updatedName = User.UpdateName(name);

        }
    }
}
