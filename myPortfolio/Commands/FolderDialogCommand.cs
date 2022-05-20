using myPortfolio.ViewModels;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class FolderDialogCommand : BaseCommand
    {

        private readonly AddAppViewModel _addAppViewModel;

        public FolderDialogCommand(AddAppViewModel addAppViewModel)
        {
            _addAppViewModel = addAppViewModel;
        }

        public override void Execute(object parameter)
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();

            bool? result = dialog.ShowDialog();

            if(result == true)
            {
                _addAppViewModel.Folder = dialog.SelectedPath;
            }

        }
    }
}
