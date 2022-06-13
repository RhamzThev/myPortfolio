using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace myPortfolio.Services
{
    /// <summary>
    /// Manages the navigation between view models.
    /// </summary>
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

        /// <summary>
        /// A method that's called when the current view model has changed.
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
