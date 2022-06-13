using MahApps.Metro.IconPacks;
using myPortfolio.Commands;
using myPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace myPortfolio.ViewModels
{
    /// <summary>
    /// Class <c>MainWindowViewModel</c> inherits <c>BaseViewModel</c>. Handles the Data binding for <c>MainWindow</c>.
    /// Reference of navigation contained here.
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        public Navigation _navigation;

        public BaseViewModel CurrentViewModel => _navigation.CurrentViewModel;

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private WindowState _windowState;
        public WindowState WindowState
        {
            get {return _windowState;}
            set
            {
                _windowState = value;
                OnPropertyChanged(nameof(WindowState));
            }
        }

        private PackIconCodiconsKind _maximize;
        public PackIconCodiconsKind Maximize
        {
            get
            {
                return _maximize;
            }
            set
            {
                _maximize = value;
                OnPropertyChanged(nameof(Maximize));
            }
        }

        public ICommand MinimizeCommand { get; } 
        public ICommand MaximizeCommand { get; } 
        public ICommand CloseCommand { get; }

        /// <summary>
        /// Cosntructor for <c>MainWindowViewModel</c>.
        /// Initial view model is defined here.
        /// </summary>
        /// <param name="navigation">MainWindowViewModel</param>
        public MainWindowViewModel(Navigation navigation)
        {
            _navigation = navigation;
            _navigation.CurrentViewModelChanged += OnCurrentViewModelChanged;

            _maximize = PackIconCodiconsKind.ChromeMaximize;
            _windowState = WindowState.Normal;

            MinimizeCommand = new RelayCommand(o => ((Window)o).WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(o =>
            {
                if (((MainWindowViewModel)o).WindowState == WindowState.Normal)
                {
                    ((MainWindowViewModel)o).WindowState = WindowState.Maximized;
                    ((MainWindowViewModel)o).Maximize = PackIconCodiconsKind.ChromeRestore;
                }
                else
                {
                    ((MainWindowViewModel)o).WindowState = WindowState.Normal;
                    ((MainWindowViewModel)o).Maximize = PackIconCodiconsKind.ChromeMaximize;
                }
            });
            CloseCommand = new RelayCommand(o => ((Window)o).Close());

        }

    }
}
