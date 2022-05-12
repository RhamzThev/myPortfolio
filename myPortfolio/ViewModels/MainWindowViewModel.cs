using myPortfolio.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public Navigation _navigation;

        public BaseViewModel CurrentViewModel => _navigation.CurrentViewModel;

        public MainWindowViewModel(Navigation navigation)
        {
            _navigation = navigation;
            _navigation.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
