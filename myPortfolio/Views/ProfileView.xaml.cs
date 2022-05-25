using myPortfolio.Services;
using myPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(UserControl_Loaded);
        }

        private void NameChanged(object sender, TextChangedEventArgs e)
        {
            if(((TextBox)sender).Text == "")
            {
                string displayName = ((ProfileViewModel)this.DataContext).DisplayName;
                Miscellaneous.ChangeTextBox(sender, displayName);
            }
            else
            {
                ((TextBox)sender).Background = null; 
            }
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((ProfileViewModel)this.DataContext).Password = ((PasswordBox)sender).Password; }

            if(((PasswordBox)sender).Password == "")
            {
                Miscellaneous.ChangePasswordBox(sender, "Change Password");
            }
            else
            {
                ((PasswordBox)sender).Background = null;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            Popup passwordPopup = (Popup)this.FindName("passwordPopup");

            if(window != null)
            {
                window.LocationChanged += delegate (object sender, EventArgs args)
                {
                    double offset = passwordPopup.HorizontalOffset;
                    passwordPopup.HorizontalOffset = offset + 1;
                    passwordPopup.HorizontalOffset = offset;
                };
            }
        }
    }
}
