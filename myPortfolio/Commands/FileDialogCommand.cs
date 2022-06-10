using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using myPortfolio.ViewModels;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>FileDialogCommand</c> inherits <c>BaseCommand</c>. Executes command to open the file dialog. 
    /// </summary>
    public class FileDialogCommand : BaseCommand
    {
        private readonly AddAppViewModel _addAppViewModel;

        /// <summary>
        /// Constructor for <c>FileDialogCommand</c>. 
        /// </summary>
        /// <param name="addAppViewModel"></param>
        public FileDialogCommand(AddAppViewModel addAppViewModel)
        {
            _addAppViewModel = addAppViewModel;
        }

        public override void Execute(object parameter)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            bool? result = dialog.ShowDialog();

            if(result == true)
            {
                _addAppViewModel.File = dialog.FileName;
            }
        }
    }
}
