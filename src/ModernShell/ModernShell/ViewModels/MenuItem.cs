using System.Collections.Generic;
using System.Linq;
using ModernShell.Controls.Menu;

namespace ModernShell.ViewModels
{
    public class MenuItem : INavigationMenuDescriptor
    {
        public MenuItem(string text, bool isRoot = false, string icon = null, ICollection<INavigationMenuDescriptor> items = null)
        {
            IsRoot = isRoot;
            Text = text;
            Icon = icon;
            Items = items;
        }

        public ICollection<INavigationMenuDescriptor> Items { get; }

        public bool IsRoot { get; }

        public bool HasItems
        {
            get { return Items?.Any() ?? false; }
        }

        public string Icon { get; }

        public string Text { get; }
    }
}