using myPortfolio.Models;
using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class UpdatePasswordConfirmationCommand : BaseCommand
    {
        private ProfileViewModel _profileViewModel;
        private Navigation _navigation;

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

            bool updatedPassword = User.UpdatePassword(password, previousPassword);

            _profileViewModel.Password = "";
            _profileViewModel.PreviousPassword = "";

        }
    }
}
