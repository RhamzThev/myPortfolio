using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class ExecuteAppCommand : BaseCommand
    {
        private HomeViewModel _homeViewModel;

        public ExecuteAppCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
        }

        public override void Execute(object parameter)
        {
            Models.App app = _homeViewModel.SelectedItem;
            app.ExecuteApp();
        }
    }
}
