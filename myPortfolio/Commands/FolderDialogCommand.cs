using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class FolderDialogCommand : BaseCommand
    {

        private readonly AddAppViewModel addAppViewModel;

        public FolderDialogCommand(AddAppViewModel addAppViewModel)
        {
            this.addAppViewModel = addAppViewModel;
        }

        public override void Execute(object parameter)
        {
        }
    }
}
