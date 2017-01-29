using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ModernShell.Controls.Menu
{
    public class SmallNavigationMenu : Control
    {
        static SmallNavigationMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (SmallNavigationMenu), new FrameworkPropertyMetadata(typeof (SmallNavigationMenu)));
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof (IEnumerable<INavigationMenuDescriptor>),
            typeof (SmallNavigationMenu),
            new PropertyMetadata(null, ItemsSourcePropertyChanged));

        public static readonly DependencyProperty MenuItemsProperty = DependencyProperty.Register(
            "MenuItems",
            typeof (ICollection<SmallNavigationMenuItem>),
            typeof (SmallNavigationMenu),
            new PropertyMetadata(null, MenuItemsPropertyChanged));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof (INavigationMenuDescriptor),
            typeof (SmallNavigationMenu),
            new FrameworkPropertyMetadata(null, OnSelectedItemChanged)
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public static readonly DependencyProperty SelectedMenuItemProperty = DependencyProperty.Register(
            "SelectedMenuItem",
            typeof (SmallNavigationMenuItem),
            typeof (SmallNavigationMenu),
            new PropertyMetadata(null, SelectedMenuItemPropertyChanged));

        public static readonly DependencyProperty RootItemTemplateProperty = DependencyProperty.Register(
            "RootItemTemplate",
            typeof(DataTemplate),
            typeof(SmallNavigationMenu),
            new PropertyMetadata(null));

        public static readonly DependencyProperty LeafItemTemplateProperty = DependencyProperty.Register(
            "LeafItemTemplate", 
            typeof(DataTemplate), 
            typeof(SmallNavigationMenu), 
            new PropertyMetadata(null));

        public DataTemplate RootItemTemplate
        {
            set { SetValue(RootItemTemplateProperty, value); }
            get { return (DataTemplate) GetValue(RootItemTemplateProperty); }
        }

        public DataTemplate LeafItemTemplate
        {
            get { return (DataTemplate)GetValue(LeafItemTemplateProperty); }
            set { SetValue(LeafItemTemplateProperty, value); }
        }

        public ICollection<SmallNavigationMenuItem> MenuItems
        {
            set { SetValue(MenuItemsProperty, value); }
            get { return (ICollection<SmallNavigationMenuItem>) GetValue(MenuItemsProperty); }
        }

        public SmallNavigationMenuItem SelectedMenuItem
        {
            set { SetValue(SelectedMenuItemProperty, value); }
            get { return (SmallNavigationMenuItem) GetValue(SelectedMenuItemProperty); }
        }

        public IEnumerable<INavigationMenuDescriptor> ItemsSource
        {
            set { SetValue(ItemsSourceProperty, value); }
            get { return (IEnumerable<INavigationMenuDescriptor>) GetValue(ItemsSourceProperty); }
        }

        public INavigationMenuDescriptor SelectedItem
        {
            set { SetValue(SelectedItemProperty, value); }
            get { return (INavigationMenuDescriptor) GetValue(SelectedItemProperty); }
        }

        private void UnselectOldSelectedMenuItems(SmallNavigationMenuItem selectedMenuItem)
        {
            var oldSelectedMenuItems = MenuItems.Where(item => item.MenuItems != null)
                .SelectMany(item => item.MenuItems)
                .Concat(MenuItems)
                .Where(item => !Equals(item, selectedMenuItem))
                .Where(item => item.IsSelected)
                .ToArray();

            foreach (var item in oldSelectedMenuItems)
            {
                item.ClosePopup();

                if (item.MenuItems?.All(x => !Equals(x, selectedMenuItem)) ?? true)
                {
                    item.IsSelected = false;
                }
            }
        }

        private static void SelectedMenuItemPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            //var menu = (SmallNavigationMenu) sender;
            //var selectedNavigationItem = args.NewValue as SmallNavigationMenuItem;
            //menu.MarkAsSelected(selectedNavigationItem);
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var menu = (SmallNavigationMenu)d;
            var selectedDescriptor = e.NewValue as INavigationMenuDescriptor;
            menu.MarkAsSelected(selectedDescriptor);
        }

        private static void MenuItemsPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
        }

        private static void ItemsSourcePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var control = (SmallNavigationMenu) sender;
            control.SetupMenuItems(args.NewValue as IEnumerable<INavigationMenuDescriptor>);
        }

        private void MarkAsSelected(INavigationMenuDescriptor menuItemDescriptor)
        {
            if (menuItemDescriptor == null)
            {
                return;
            }

            var navigationItem = MenuItems.Where(x => x.MenuItems != null)
                                          .SelectMany(x => x.MenuItems)
                                          .Concat(MenuItems)
                                          .SingleOrDefault(x => x.DataContext == menuItemDescriptor);

            if (navigationItem != null)
            {
                navigationItem.IsSelected = true;
                foreach (var item in MenuItems)
                {
                    if (item.MenuItems?.Any(x => Equals(x, navigationItem)) ?? false)
                    {
                        item.IsSelected = true;
                    }
                }

                UnselectOldSelectedMenuItems(navigationItem);
            }
        }

        private void SetupMenuItems(IEnumerable<INavigationMenuDescriptor> navigationMenuDescriptors)
        {
            var menuDescriptors = navigationMenuDescriptors?.ToArray();
            var menuItems = new List<SmallNavigationMenuItem>();
            CalculateMenuItems(menuDescriptors, menuItems);
            MenuItems = menuItems;
            MenuItems?.FirstOrDefault()?.MarkAsSelected();
        }

        private void OnSmallNavigationMenuItemSelectionChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            e.Handled = true;
            var selectedNavigationItem = sender as SmallNavigationMenuItem;

            if (selectedNavigationItem != null && e.NewValue)
            {
                SelectMenuItem(selectedNavigationItem);
            }
        }

        private void SelectMenuItem(SmallNavigationMenuItem item)
        {
            SelectedItem = item.DataContext as INavigationMenuDescriptor;
            SelectedMenuItem = item;
            UnselectOldSelectedMenuItems(item);
        }

        private void CalculateMenuItems(ICollection<INavigationMenuDescriptor> menuDescriptors, ICollection<SmallNavigationMenuItem> menuItems)
        {
            if (menuDescriptors == null)
            {
                return;
            }

            foreach (var menuDescriptor in menuDescriptors)
            {
                var menuItem = new SmallNavigationMenuItem {DataContext = menuDescriptor};
                menuItem.SetBinding(SmallNavigationMenuItem.ItemTemplateProperty, new Binding(menuDescriptor.IsRoot ? "RootItemTemplate" : "LeafItemTemplate") {Source = this});
                menuItem.AddHandler(SmallNavigationMenuItem.SelectionChangedEvent, new RoutedPropertyChangedEventHandler<bool>(OnSmallNavigationMenuItemSelectionChanged));
                menuItems.Add(menuItem);

                if (!(menuDescriptor.Items?.Any() ?? false))
                {
                    continue;
                }

                if (menuItem.MenuItems == null)
                {
                    menuItem.MenuItems = new List<SmallNavigationMenuItem>();
                }

                CalculateMenuItems(menuDescriptor.Items, menuItem.MenuItems);
            }
        }

        public void ClosePopups()
        {
            foreach (var menuItem in MenuItems)
            {
                menuItem.ClosePopup();
            }
        }
    }
}