using System.Windows;
using System.Windows.Controls;

namespace ModernShell.Controls.Menu
{
    public class NavigationMenu : Control
    {
        static NavigationMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationMenu), new FrameworkPropertyMetadata(typeof(NavigationMenu)));
        }


        public INavigationMenuDescriptor[] ItemsSource { get; set; }

        public INavigationMenuDescriptor SelectedItem { get; set; }
    }
}
