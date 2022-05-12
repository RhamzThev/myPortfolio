using myPortfolio.Stores;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace myPortfolio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            Navigation navigation = new Navigation();
            navigation.CurrentViewModel = new StartViewModel(navigation);

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(navigation)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
