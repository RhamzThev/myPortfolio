using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class AddAppCommand : BaseCommand
    {
        private AddAppViewModel _addAppViewModel;
        private Navigation _navigation;

        public AddAppCommand(AddAppViewModel addAppViewModel, Navigation navigation)
        {
            _addAppViewModel = addAppViewModel;
            _navigation = navigation;
        }

        public override void Execute(object parameter)
        {
            string name = _addAppViewModel.Name;
            string description = _addAppViewModel.Description;
            string sourceFolder = _addAppViewModel.Folder;
            string sourceExePath = _addAppViewModel.File;

            bool addedApp = Models.App.AddApp(name, description, sourceFolder, sourceExePath);
            
            if(addedApp)
            {
                _navigation.CurrentViewModel = new HomeViewModel(_navigation);
            }

        }
    }
}
