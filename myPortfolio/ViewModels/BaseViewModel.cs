using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace myPortfolio.ViewModels
{
    /// <summary>
    /// Class <c>BaseViewModel</c> inherits <c>INotifyPropertyChanged</c>. Is the base class for view models.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// A method that's called when a property has changed.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
