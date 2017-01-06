using System;
using System.Windows;
using ModernShell.Controls.Buttons;

namespace ModernShell.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
            HamburgerMenuButton.AddHandler(HamburgerMenuButton.IsOpenedEvent, new RoutedPropertyChangedEventHandler<bool>(OnHamburgerMenuClicked));
        }

        private void OnHamburgerMenuClicked(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            var states = VisualStateManager.GetVisualStateGroups(LayoutRoot);
            var stateName = e.NewValue ? "Expanded" : "Collapsed";
            
            var result = VisualStateManager.GoToElementState(LayoutRoot, stateName, true);
            InternalSmallNavMenu.Visibility = !e.NewValue ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
