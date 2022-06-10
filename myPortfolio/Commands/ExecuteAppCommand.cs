using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>ExecuteAppCommand</c> inherits <c>BaseCommand</c>. Executes command to execute the selected external application. 
    /// </summary>
    public class ExecuteAppCommand : BaseCommand
    {
        private HomeViewModel _homeViewModel;

        /// <summary>
        /// Constructor for <c>ExecuteAppCommand</c>. 
        /// </summary>
        /// <param name="homeViewModel"></param>
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
