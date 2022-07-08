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
                ChangePasswordBox(sender, "Password");
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
                ChangeTextBox(sender, "Username");
            }
            else
            {
                ((TextBox)sender).Background = null;
            }
        }

        private void ChangePasswordBox(object sender, string text)
        {
            VisualBrush visualBrush = SetBlock(text);

            // PAINT IMAGE
            ((PasswordBox)sender).Background = visualBrush;
        }

        private void ChangeTextBox(object sender, string text)
        {
            VisualBrush visualBrush = SetBlock(text);

            // PAINT IMAGE
            ((TextBox)sender).Background = visualBrush;
        }

        private VisualBrush SetBlock(string text)
        {
            // CREATE VISUAL BRUSH
            VisualBrush visualBrush = new VisualBrush();
            visualBrush.Stretch = Stretch.None;
            visualBrush.AlignmentX = AlignmentX.Left;

            // CREATE VISUAL (TEXT BOX)
            TextBlock textblock = new TextBlock();
            textblock.Foreground = new SolidColorBrush(Colors.White);
            textblock.Opacity = 0.5;
            textblock.Text = text;

            // SET TEXTBOX TO BE BRUSH'S VISUAL
            visualBrush.Visual = textblock;

            return visualBrush;
        }
    }
}
