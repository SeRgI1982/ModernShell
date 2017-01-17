using System.Collections.Generic;
using ModernShell.Controls.Menu;

namespace ModernShell.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private ICollection<MenuItem> _items;
        private MenuItem _selectedItem;

        public ShellViewModel()
        {
            GenerateMenuItems();
        }

        public ICollection<MenuItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public MenuItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        private void GenerateMenuItems()
        {
            var menu1 = new MenuItem("Dashboard", "home");
            var menu2 = new MenuItem("Layouts", "airplay",
                                     new List<INavigationMenuDescriptor>
                                     {
                                         new MenuItem("Layout Options"),
                                         new MenuItem("Boxed Layout"),
                                         new MenuItem("Inner Boxed Layout"),
                                         new MenuItem("Extended Layout"),
                                         new MenuItem("RTL Layout"),
                                         new MenuItem("Horizontal Menu"),
                                         new MenuItem("Horizontal Menu & Boxed"),
                                     });
            var menu3 = new MenuItem("Tables", "playlist_add_check", new List<INavigationMenuDescriptor>
                                     {
                                         new MenuItem("Basic Tables"),
                                         new MenuItem("Data Tables"),
                                     });
            var menu4 = new MenuItem("Charts", "timeline");
            Items = new List<MenuItem> {menu1, menu2, menu3, menu4};
        }
    }
}
