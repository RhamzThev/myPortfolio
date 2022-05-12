using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Commands
{
    public class NavigateCommand : BaseCommand
    {

        private readonly Navigation _navigation;
        private readonly BaseViewModel _baseViewModel;

        public NavigateCommand(Navigation navigation, BaseViewModel baseViewModel)
        {
            _navigation = navigation;
            _baseViewModel = baseViewModel;
        }

        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = _baseViewModel;
        }
    }
}
