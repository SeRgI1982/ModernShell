using System.Collections.Generic;

namespace ModernShell.Controls.Menu
{
    public interface INavigationMenuDescriptor
    {
        string Text { get; }
        ICollection<INavigationMenuDescriptor> Items { get; }
    }
}