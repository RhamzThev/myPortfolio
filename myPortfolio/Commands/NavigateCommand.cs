using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    /// <summary>
    /// Class <c>NavigateCommand</c> inherits <c>BaseCommand</c> with a generic type of <c>BaseViewModel</c>, <c>TViewModel</c>.
    /// This execute commands to navigate through various ViewModels.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class NavigateCommand<TViewModel> : BaseCommand where TViewModel : BaseViewModel
    {

        private readonly Navigation _navigation;
        private readonly Func<TViewModel> _viewModel;

        /// <summary>
        /// Constructor for <c>NavigateCommand</c>. 
        /// </summary>
        /// <param name="navigation"></param>
        /// <param name="viewModel"></param>
        public NavigateCommand(Navigation navigation, Func<TViewModel> viewModel)
        {
            _navigation = navigation;
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = _viewModel();
        }
    }
}
