using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class NavigateCommand<TViewModel> : BaseCommand where TViewModel : BaseViewModel
    {

        private readonly Navigation _navigation;
        private readonly Func<TViewModel> _viewModel;

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
