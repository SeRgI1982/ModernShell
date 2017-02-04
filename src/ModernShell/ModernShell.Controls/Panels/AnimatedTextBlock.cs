using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ModernShell.Controls.Panels
{
    public class AnimatedTextBlock : TextBlock
    {
        public AnimatedTextBlock()
        {
            Loaded += OnAnimatedTextBlockLoaded;
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(AnimatedTextBlock),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ValueFormatProperty = DependencyProperty.Register(
            "ValueFormat",
            typeof(string),
            typeof(AnimatedTextBlock),
            new PropertyMetadata("{0:N2}"));

        public double Value
        {
            set { SetValue(ValueProperty, value); }
            get { return (double)GetValue(ValueProperty); }
        }

        public string ValueFormat
        {
            set { SetValue(ValueFormatProperty, value); }
            get { return (string)GetValue(ValueFormatProperty); }
        }

        private void OnAnimatedTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            var binding = new Binding("Value") {StringFormat = ValueFormat, Source = this};
            SetBinding(AnimatedTextBlock.TextProperty, binding);
        }
    }
}