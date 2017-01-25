using System;
using System.Windows;
using ModernShell.Controls.Buttons;

namespace ModernShell.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
            HamburgerMenuButton.AddHandler(MaterialIconButton.IsOpenedEvent, new RoutedPropertyChangedEventHandler<bool>(OnHamburgerMenuClicked));
            NotificationMenuButton.AddHandler(MaterialIconButton.IsOpenedEvent, new RoutedPropertyChangedEventHandler<bool>(OnNotificationMenuClicked));
        }

        private void OnNotificationMenuClicked(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            var shouldOpenNotifyPanel = e.NewValue;

            VisualStateManager.GoToElementState(LayoutRoot,
                shouldOpenNotifyPanel ? "OpenNotificationPanel" : "CloseNotificationPanel", true);
        }

        private void OnHamburgerMenuClicked(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            var shouldOpenMenuPanel = e.NewValue;

            VisualStateManager.GoToElementState(LayoutRoot, shouldOpenMenuPanel ? "OpenMenuPanel" : "CloseMenuPanel",
                true);
        }
    }
}
