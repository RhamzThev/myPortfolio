using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace myPortfolio.Services
{
    public class Miscellaneous
    {
        public static void ChangePasswordBox(object sender, string text)
        {
            VisualBrush visualBrush = SetBlock(text);

            // PAINT IMAGE
            ((PasswordBox)sender).Background = visualBrush;
        }

        public static void ChangeTextBox(object sender, string text)
        {
            VisualBrush visualBrush = SetBlock(text);

            // PAINT IMAGE
            ((TextBox)sender).Background = visualBrush;
        }

        private static VisualBrush SetBlock(string text)
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
