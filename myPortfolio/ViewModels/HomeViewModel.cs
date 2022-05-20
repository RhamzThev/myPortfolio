using myPortfolio.Commands;
using myPortfolio.Models;
using myPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace myPortfolio.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {

        public string Name
        {
            get
            {
                return User.Name;
            }
        }

        public List<Models.App> Apps {
            get
            {
                return Models.App.Apps;
            } 
        }

        private Models.App _selectedItem;
        public Models.App SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public Visibility Visibility 
        { 
            get 
            { 
                if(string.Equals(User.Username, "rhamzthev"))
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            } 
        }

        public ICommand ProfileCommand { get; }
        public ICommand AddAppCommand { get; }
        public ICommand ExecuteAppCommand { get; }
        public ICommand SignOutCommand { get; }

        public HomeViewModel(Navigation navigation)
        {
            ProfileCommand = new ProfileCommand(navigation);
            AddAppCommand = new NavigateCommand<AddAppViewModel>(navigation, () => new AddAppViewModel(navigation));
            ExecuteAppCommand = new ExecuteAppCommand(this);
            SignOutCommand = new SignOutCommand(navigation);
        }
    }
}
