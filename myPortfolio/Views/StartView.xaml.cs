using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace myPortfolio.Views
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : UserControl
    {
        public StartView()
        {
            InitializeComponent();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((StartViewModel)this.DataContext).Password = ((PasswordBox)sender).Password; }

            if (string.IsNullOrEmpty(((PasswordBox)sender).Password))
            {
                Miscellaneous.ChangePasswordBox(sender, "Password");
            }
            else
            {
                ((PasswordBox)sender).Background = null;
            }
        }

        private void UsernameChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                Miscellaneous.ChangeTextBox(sender, "Username");
            }
            else
            {
                ((TextBox)sender).Background = null;
            }
        }
    }
}
