using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace myPortfolio.Services
{
    /// <summary>
    /// A class where miscellaneous helper functions will be contained.
    /// </summary>
    public class Miscellaneous
    {
        /// <summary>
        /// Changes the password box's background text.
        /// </summary>
        /// <param name="sender">The sender object (supposed to be <c>PasswordBox</c>)</param>
        /// <param name="text">What the background text will be changed to.</param>
        public static void ChangePasswordBox(object sender, string text)
        {
            VisualBrush visualBrush = SetBlock(text);

            // PAINT IMAGE
            ((PasswordBox)sender).Background = visualBrush;
        }

        /// <summary>
        /// Changes the text box's background text.
        /// </summary>
        /// <param name="sender">The sender object (supposed to be <c>TextBox</c>)</param>
        /// <param name="text">What the background text will be changed to.</param>
        public static void ChangeTextBox(object sender, string text)
        {
            VisualBrush visualBrush = SetBlock(text);

            // PAINT IMAGE
            ((TextBox)sender).Background = visualBrush;
        }

        /// <summary>
        /// Set's background text to given text.
        /// </summary>
        /// <param name="text">The given text</param>
        /// <returns>The new background as a <c>VisualBrush</c>.</returns>
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
