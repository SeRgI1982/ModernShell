using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ModernShell.Controls.Buttons;
using ModernShell.Controls.Menu;

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
            EventManager.RegisterClassHandler(typeof(Window), Window.PreviewMouseDownEvent, new MouseButtonEventHandler(OnPreviewMouseDown));
        }

        private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is SmallNavigationMenu)
            {
                return;
            }

            smallNavMenu.ClosePopups();
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
