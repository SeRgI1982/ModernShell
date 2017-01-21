using System.Collections.Generic;

namespace ModernShell.Controls.Menu
{
    public interface INavigationMenuDescriptor
    {
        bool IsRoot { get; }
        bool HasItems { get; }
        string Icon { get; }
        string Text { get; }
        ICollection<INavigationMenuDescriptor> Items { get; }
    }
}