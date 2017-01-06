namespace ModernShell.Controls.Menu
{
    public interface INavigationMenuDescriptor
    {
        string Text { get; }
        INavigationMenuDescriptor[] Items { get; }
    }
}