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
    /// Interaction logic for AddAppView.xaml
    /// </summary>
    public partial class AddAppView : UserControl
    {
        public AddAppView()
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

        private void DescriptionChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                Miscellaneous.ChangeTextBox(sender, "Description");
            }
            else
            {
                ((TextBox)sender).Background = null;
            }
        }
    }
}
