using myPortfolio.Commands;
using myPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace myPortfolio.ViewModels
{
    /// <summary>
    /// Class <c>AddAppViewModel</c> inherits <c>BaseViewModel</c>. Handles the Data binding for AddAppView.
    /// </summary>
    public class AddAppViewModel : BaseViewModel
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

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _folder;
        public string Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
                OnPropertyChanged(nameof(Folder));
            }
        }

        private string _file;
        public string File
        {
            get
            {
                return _file;
            }
            set
            {
                _file = value;
                OnPropertyChanged(nameof(File));
            }
        }

        public ICommand BackCommand { get; }
        public ICommand ChooseFolderCommand { get; }
        public ICommand ChooseFileCommand { get; }
        public ICommand AddAppCommand { get; }

        /// <summary>
        /// Constructor for <c>AddAppViewModel</c>.
        /// </summary>
        /// <param name="navigation">Class used to navigate across various view models.</param>
        public AddAppViewModel(Navigation navigation)
        {
            BackCommand = new NavigateCommand<HomeViewModel>(navigation, () => new HomeViewModel(navigation));
            ChooseFolderCommand = new FolderDialogCommand(this);
            ChooseFileCommand = new FileDialogCommand(this);
            AddAppCommand = new AddAppCommand(this, navigation);
        }
    }    
}
