using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Expression.Interactivity.Core;
using ModernShell.Controls.Buttons;

namespace ModernShell.Controls.TextBoxes
{
    public class WatermarkTextBox : TextBox
    {
        private MaterialIconButton _clearButton;

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

        /// <summary>
        /// Is called when a control template is applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            _clearButton = Template.FindName("PART_ClearButton", this) as MaterialIconButton;
            if (_clearButton != null)
            {
                _clearButton.Command = new ActionCommand(Clear);
            }
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.UIElement.MouseLeftButtonDown"/>�routed event is raised on this element. Implement this method to add class handling for this event. 
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that the left mouse button was pressed.</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Mouse.PreviewMouseDown"/> attached�routed event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event. 
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that one or more mouse buttons were pressed.</param>
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            var mousePosition = e.GetPosition(this);
            var buttonPosition = _clearButton.TranslatePoint(new Point(0, 0), this);

            if (mousePosition.X >= buttonPosition.X && mousePosition.X <= (buttonPosition.X + _clearButton.ActualWidth) &&
                mousePosition.Y >= buttonPosition.Y && mousePosition.Y <= (buttonPosition.Y + _clearButton.ActualHeight))
            {
                _clearButton.Command.Execute(null);
            }

            base.OnPreviewMouseDown(e);
        }
    }
}
