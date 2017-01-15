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
            var menu1 = new MenuItem("Menu 1", null);
            var menu2 = new MenuItem("Menu 2",
                                     new List<INavigationMenuDescriptor>
                                     {
                                         new MenuItem("Menu 2.1", null),
                                         new MenuItem("Menu 2.2", null),
                                         new MenuItem("Menu 2.3", null)
                                     });
            var menu3 = new MenuItem("Menu 3", null);
            Items = new List<MenuItem> {menu1, menu2, menu3};
        }
    }
}
