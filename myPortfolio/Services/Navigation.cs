using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Services
{
    public class Navigation
    {

        public event Action CurrentViewModelChanged;

        public BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel 
        { 
            get => _currentViewModel; 
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
