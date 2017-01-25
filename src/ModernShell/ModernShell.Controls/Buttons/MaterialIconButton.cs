using System.Windows;
using System.Windows.Controls;

namespace ModernShell.Controls.Buttons
{
    public class MaterialIconButton : Button
    {   
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(
            "IsOpen", 
            typeof(bool), 
            typeof(MaterialIconButton), 
            new PropertyMetadata(false));

        public static readonly DependencyProperty DisableTransitionsProperty = DependencyProperty.Register(
            "DisableTransitions", 
            typeof(bool), 
            typeof(MaterialIconButton), 
            new PropertyMetadata(false));

        public static readonly RoutedEvent IsOpenedEvent = EventManager.RegisterRoutedEvent(
            "IsOpened",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<bool>),
            typeof(MaterialIconButton));

        static MaterialIconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaterialIconButton), new FrameworkPropertyMetadata(typeof(MaterialIconButton)));
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public bool DisableTransitions
        {
            get { return (bool)GetValue(DisableTransitionsProperty); }
            set { SetValue(DisableTransitionsProperty, value); }
        }

        public event RoutedPropertyChangedEventHandler<bool> IsOpened
        {
            add { AddHandler(IsOpenedEvent, value); }
            remove { RemoveHandler(IsOpenedEvent, value); }
        }

        protected override void OnClick()
        {
            base.OnClick();
            var oldValue = IsOpen;
            IsOpen = !IsOpen;

            RaiseEvent(new RoutedPropertyChangedEventArgs<bool>(oldValue, IsOpen, IsOpenedEvent));
        }
    }
}
