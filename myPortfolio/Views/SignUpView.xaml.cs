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
            if (((TextBox)sender).Text == "")
            {
                ChangeTextBox(sender, "Name");
            }
            else
            {
                ((TextBox)sender).Background = null;
            }
        }

        private void UsernameChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ChangeTextBox(sender, "Username");
            }
            else
            {
                ((TextBox)sender).Background = null;
            }
        }

        private void PasswordChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ChangeTextBox(sender, "Password");
            }
            else
            {
                ((TextBox)sender).Background = null;
            }
        }

        private void RepeatPasswordChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ChangeTextBox(sender, "Repeat Password");
            }
            else
            {
                ((TextBox)sender).Background = null;
            }
        }

        private void ChangeTextBox(object sender, string text)
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

            // PAINT IMAGE
            ((TextBox)sender).Background = visualBrush;
        }
    }
}
