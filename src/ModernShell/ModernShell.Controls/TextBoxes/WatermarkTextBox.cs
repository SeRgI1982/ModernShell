using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModernShell.Controls.TextBoxes
{
    public class WatermarkTextBox : TextBox
    {
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
            "Watermark",
            typeof(string),
            typeof(WatermarkTextBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty WatermarkForegroundProperty = DependencyProperty.Register(
            "WatermarkForeground",
            typeof(Brush),
            typeof(WatermarkTextBox),
            new PropertyMetadata(null));

        public static readonly DependencyProperty WatermarkMarginProperty = DependencyProperty.Register(
            "WatermarkMargin",
            typeof(Thickness),
            typeof(WatermarkTextBox),
            new PropertyMetadata(null));
        
        static WatermarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkTextBox), new FrameworkPropertyMetadata(typeof(WatermarkTextBox)));
        }

        public string Watermark
        {
            set { SetValue(WatermarkProperty, value); }
            get { return (string)GetValue(WatermarkProperty); }
        }

        public Brush WatermarkForeground
        {
            set { SetValue(WatermarkForegroundProperty, value); }
            get { return (Brush)GetValue(WatermarkForegroundProperty); }
        }

        public Thickness WatermarkMargin
        {
            set { SetValue(WatermarkMarginProperty, value); }
            get { return (Thickness)GetValue(WatermarkMarginProperty); }
        }
    }
}
