using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using ModernShell.Controls.Menu;
using ModernShell.Models;

namespace ModernShell.ViewModels
{
    public class ShellViewModel : NotifyPropertyBase
    {
        private string _searchPhrase;
        private double _value1;
        private double _value2;
        private double _value3;
        private double _value4;
        private IEnumerable<MenuItem> _items;
        private ICollectionView _contacts; 
        private MenuItem _selectedItem;

        public ShellViewModel()
        {
            GenerateMenuItems();
            GenerateContacts();
        }

        public IEnumerable<MenuItem> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public ICollectionView Contacts
        {
            get { return _contacts; }
            set { SetProperty(ref _contacts, value); }
        } 

        public MenuItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    if (SelectedItem.Text == "Widgets")
                    {
                        Value1 = 78;
                        Value2 = 26;
                        Value3 = 44;
                        Value4 = 95;
                    }
                    else
                    {
                        Value1 = 0;
                        Value2 = 0;
                        Value3 = 0;
                        Value4 = 0;
                    }
                }
            }
        }

        public double Value1
        {
            get { return _value1; }
            set { SetProperty(ref _value1, value); }
        }

        public double Value2
        {
            get { return _value2; }
            set { SetProperty(ref _value2, value); }
        }

        public double Value3
        {
            get { return _value3; }
            set { SetProperty(ref _value3, value); }
        }

        public double Value4
        {
            get { return _value4; }
            set { SetProperty(ref _value4, value); }
        }

        public string SearchPhrase
        {
            get { return _searchPhrase; }
            set
            {
                if (SetProperty(ref _searchPhrase, value))
                {
                    Contacts.Refresh();
                }
            }
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

        private void GenerateContacts()
        {
            var contact1 = new Contact(1, "Jane", "Smith", "d2x.jpg");
            var msg11 = new Message(11, 1, "How are you ?", DateTime.Now);
            var msg12 = new Message(12, 1, "How are you ?", DateTime.Now);
            var msg13 = new Message(13, 1, "How are you ?", DateTime.Now);
            contact1.AddMessages(new[] { msg11, msg12, msg13 });
            contact1.ChangeStatus(Statuses.Away);

            var contact2 = new Contact(2, "David", "Nester", "a2x.jpg");
            var msg2 = new Message(1, 2, "Sorry, I can't talk right now.", DateTime.Now);
            contact2.AddMessages(new[] { msg2 });
            contact2.ChangeStatus(Statuses.DoNotDisturb);

            var contact3 = new Contact(3, "John", "Doe", "c2x.jpg");
            var msg3 = new Message(1, 3, "Hello, are you there ?", DateTime.Now);
            contact3.AddMessages(new[] { msg3 });
            contact3.ChangeStatus(Statuses.Offline);

            var contact4 = new Contact(4, "Alexander", "Newman", "h2x.jpg");
            var msg4 = new Message(1, 4, "Please, call me later!", DateTime.Now);
            contact4.AddMessages(new[] { msg4 });
            contact4.ChangeStatus(Statuses.Online);

            var contacts = new List<Contact>();
            contacts.AddRange(new[] { contact1, contact2, contact3, contact4 });

            Contacts = CollectionViewSource.GetDefaultView(contacts);
            Contacts.Filter = SearchContact;
        }

        private bool SearchContact(object item)
        {
            var contact = item as Contact;
            return string.IsNullOrWhiteSpace(SearchPhrase) || (contact?.FilteringRecord.Contains(SearchPhrase.ToLowerInvariant()) ?? false);
        }
    }
}
