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

        public ICommand BackCommand { get; }
        public ICommand UpdateNameCommand { get; }
        public ICommand PaswordCommand { get; }
        public ICommand DeleteAccountCommand { get; }

        public ProfileViewModel(Navigation navigation)
        {
            _name = User.Name;

            BackCommand = new NavigateCommand<HomeViewModel>(navigation, () => new HomeViewModel(navigation));
            UpdateNameCommand = new UpdateNameCommand(this, navigation);
            DeleteAccountCommand = new DeleteAccountCommand(navigation);
        }
    }
}
