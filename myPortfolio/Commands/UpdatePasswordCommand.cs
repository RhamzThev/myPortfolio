using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using myPortfolio.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class UpdatePasswordCommand : BaseCommand
    {

        private ProfileViewModel _profileViewModel;

        public UpdatePasswordCommand(ProfileViewModel profileViewModel)
        {
            _profileViewModel = profileViewModel;
        }

        public override void Execute(object parameter)
        {
            _profileViewModel.PopupIsOpen = true;
        }
    }
}
