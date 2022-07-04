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
    /// <summary>
    /// Class <c>HomeViewModel</c> inherits <c>BaseViewModel</c>. Handles the Data binding for HomeView.
    /// </summary>
    public class HomeViewModel : BaseViewModel
    {

        public char InitialName
        {
            get
            {
                return User.Name[0];
            }
        }

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

        /// <summary>
        /// Constructor for <c>HomeViewModel</c>.
        /// </summary>
        /// <param name="navigation">Class used to navigate across various view models.</param>
        public HomeViewModel(Navigation navigation)
        {
            ProfileCommand = new ProfileCommand(navigation);
            AddAppCommand = new NavigateCommand<AddAppViewModel>(navigation, () => new AddAppViewModel(navigation));
            ExecuteAppCommand = new ExecuteAppCommand(this);
        }
    }
}
