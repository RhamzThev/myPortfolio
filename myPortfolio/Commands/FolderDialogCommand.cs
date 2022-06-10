using myPortfolio.ViewModels;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>FolderDialogCommand</c> inherits <c>BaseCommand</c>. Executes command to open the folder dialog.
    /// </summary>
    public class FolderDialogCommand : BaseCommand
    {
        private readonly AddAppViewModel _addAppViewModel;

        /// <summary>
        /// Constructor for <c>FolderDialogCommand</c>. 
        /// </summary>
        /// <param name="addAppViewModel"></param>
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
