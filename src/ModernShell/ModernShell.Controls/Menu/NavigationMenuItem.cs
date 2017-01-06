using System.Windows;
using System.Windows.Controls;

namespace ModernShell.Controls.Menu
{
    public class NavigationMenuItem : Control
    {
        static NavigationMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationMenuItem), new FrameworkPropertyMetadata(typeof(NavigationMenuItem)));
        }
    }
}