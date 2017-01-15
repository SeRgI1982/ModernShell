using System.Collections.Generic;
using ModernShell.Controls.Menu;

namespace ModernShell.ViewModels
{
    public class MenuItem : INavigationMenuDescriptor
    {
        public MenuItem(string text, ICollection<INavigationMenuDescriptor> items)
        {
            Text = text;
            Items = items;
        }

        public ICollection<INavigationMenuDescriptor> Items { get; }

        public string Text { get; }
    }
}