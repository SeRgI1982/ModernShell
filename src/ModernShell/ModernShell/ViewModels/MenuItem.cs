using System.Collections.Generic;
using ModernShell.Controls.Menu;

namespace ModernShell.ViewModels
{
    public class MenuItem : INavigationMenuDescriptor
    {
        public MenuItem(string text, string icon = null, ICollection<INavigationMenuDescriptor> items = null)
        {
            Text = text;
            Icon = icon;
            Items = items;
        }

        public ICollection<INavigationMenuDescriptor> Items { get; }

        public string Icon { get; }

        public string Text { get; }
    }
}