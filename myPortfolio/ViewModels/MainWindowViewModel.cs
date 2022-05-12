using myPortfolio.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public NavigationStore _navigationStore;

        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
