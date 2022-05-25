using myPortfolio.Commands;
using myPortfolio.Models;
using myPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace myPortfolio.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Username
        {
            get
            {
                return User.Username;
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _previousPassword;
        public string PreviousPassword
        {
            get
            {
                return _previousPassword;
            }
            set
            {
                _previousPassword = value;
                OnPropertyChanged(nameof(PreviousPassword));
            }
        }

        private bool _popupIsOpen;
        public bool PopupIsOpen
        {
            get
            {
                return _popupIsOpen;
            }
            set
            {
                _popupIsOpen = value;
                OnPropertyChanged(nameof(PopupIsOpen));
            }
        }

        private string _displayName;
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public ICommand BackCommand { get; }
        public ICommand UpdateNameCommand { get; }
        public ICommand UpdatePasswordCommand { get; }
        public ICommand UpdatePasswordConfirmationCommand { get; }
        public ICommand DeleteAccountCommand { get; }

        public ProfileViewModel(Navigation navigation)
        {
            _popupIsOpen=false;
            _displayName = String.Format("Change Name ({0})", User.Name);

            BackCommand = new NavigateCommand<HomeViewModel>(navigation, () => new HomeViewModel(navigation));
            UpdateNameCommand = new UpdateNameCommand(this);
            UpdatePasswordCommand = new UpdatePasswordCommand(this);
            UpdatePasswordConfirmationCommand = new UpdatePasswordConfirmationCommand(this, navigation);
            DeleteAccountCommand = new DeleteAccountCommand(navigation);
        }
    }
}
