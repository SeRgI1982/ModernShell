using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModernShell.Controls.Menu;

namespace ModernShell.ViewModels
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        private ICollection<INavigationMenuDescriptor> _menuItems;

        public ShellViewModel()
        {
            GenerateMenuItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public ICollection<INavigationMenuDescriptor> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
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
            MenuItems = new List<INavigationMenuDescriptor> {menu1, menu2, menu3};
        }

        private void SetProperty<T>(ref T propertyField, T newPropertyValue)
        {
            if (!Equals(propertyField, newPropertyValue))
            {
                propertyField = newPropertyValue;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

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
