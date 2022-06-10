using myPortfolio.Services;
using myPortfolio.ViewModels;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>AddAppCommand</c> inherits <c>BaseCommand</c>. Executes command to add external application to database. 
    /// </summary>
    public class AddAppCommand : BaseCommand
    {
        private AddAppViewModel _addAppViewModel;
        private Navigation _navigation;

        /// <summary>
        /// Constructor for <c>AddAppCommand</c>.
        /// </summary>
        /// <param name="addAppViewModel"></param>
        /// <param name="navigation"></param>
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

            if (addedApp)
            {
                _navigation.CurrentViewModel = new HomeViewModel(_navigation);
            }

        }
    }
}
