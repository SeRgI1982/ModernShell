using System.Windows;
using System.Windows.Controls;

namespace ModernShell.Controls.Buttons
{
    public class HamburgerMenuButton : Button
    {
        public HamburgerMenuButton()
        {
            IsOpen = true;
        }

        public static readonly RoutedEvent IsOpenedEvent = EventManager.RegisterRoutedEvent(
            "IsOpened",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<bool>),
            typeof(HamburgerMenuButton));

        public event RoutedPropertyChangedEventHandler<bool> IsOpened
        {
            add { AddHandler(IsOpenedEvent, value); }
            remove { RemoveHandler(IsOpenedEvent, value); }
        }

        public bool IsOpen { get; private set; }

        /// <summary>
        /// Called when a <see cref="T:System.Windows.Controls.Button"/> is clicked. 
        /// </summary>
        protected override void OnClick()
        {
            base.OnClick();
            var oldValue = IsOpen;
            IsOpen = !IsOpen;
            RaiseEvent(new RoutedPropertyChangedEventArgs<bool>(oldValue, IsOpen, IsOpenedEvent));
        }
    }
}
