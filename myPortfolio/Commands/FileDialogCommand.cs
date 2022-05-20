using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using myPortfolio.ViewModels;

namespace myPortfolio.Commands
{
    public class FileDialogCommand : BaseCommand
    {

        private readonly AddAppViewModel _addAppViewModel;

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
