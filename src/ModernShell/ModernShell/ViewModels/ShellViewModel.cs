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
            var menu1 = new MenuItem("Dashboard", true, "home");
            var menu2 = new MenuItem("Widgets", true, "panorama_horizontal");
            var menu3 = new MenuItem("Email", true, "email");
            var menu4 = new MenuItem("Themes", true, "title");
            var menu5 = new MenuItem("Layouts", true, "airplay",
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


            var menu6 = new MenuItem("UI Elements", true, "flip");
            var menu7 = new MenuItem("Forms", true, "view_stream");
            var menu8 = new MenuItem("Grids", true, "apps");
            var menu10 = new MenuItem("Tables", true, "playlist_add_check", new List<INavigationMenuDescriptor>
                                     {
                                         new MenuItem("Basic Tables"),
                                         new MenuItem("Data Tables"),
                                     });
            var menu11 = new MenuItem("Maps", true, "location_on");
            var menu13 = new MenuItem("Charts", true, "timeline");
            var menu14 = new MenuItem("Extra", true, "layers");
            var menu15 = new MenuItem("More Levels", true, "more_horiz");
            Items = new List<MenuItem>
            {
                menu1,
                menu2,
                menu3,
                menu4,
                menu5,
                menu6,
                menu7,
                menu8,
                menu10,
                menu11,
                menu13,
                menu14,
                menu15
            };
        }
    }
}
