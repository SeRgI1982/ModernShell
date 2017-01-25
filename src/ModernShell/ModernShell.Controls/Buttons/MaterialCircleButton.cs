using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModernShell.Controls.Buttons
{
    public class MaterialCircleButton : Button
    {
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            "ImageSource", 
            typeof(ImageSource), 
            typeof(MaterialCircleButton), 
            new PropertyMetadata(default(ImageSource), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        static MaterialCircleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaterialCircleButton), new FrameworkPropertyMetadata(typeof(MaterialCircleButton)));
        }

        public ImageSource ImageSource  
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
    }
}