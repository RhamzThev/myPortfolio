using myPortfolio.ViewModels;
using myPortfolio.Services;
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
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        private void NameChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                Miscellaneous.ChangeTextBox(sender, "Name");
            }
            else
            {
                ((TextBox)sender).Background = null;
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

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((SignUpViewModel)this.DataContext).Password = ((PasswordBox)sender).Password; }

            if (string.IsNullOrEmpty(((PasswordBox)sender).Password))
            {
                Miscellaneous.ChangePasswordBox(sender, "Password");
            }
            else
            {
                ((PasswordBox)sender).Background = null;
            }
        }

        private void RepeatPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((SignUpViewModel)this.DataContext).RepeatPassword = ((PasswordBox)sender).Password; }

            if (string.IsNullOrEmpty(((PasswordBox)sender).Password))
            {
                Miscellaneous.ChangePasswordBox(sender, "Repeat Password");
            }
            else
            {
                ((PasswordBox)sender).Background = null;
            }
        }

    }
}
