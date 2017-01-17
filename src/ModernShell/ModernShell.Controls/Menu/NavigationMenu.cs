using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ModernShell.Controls.Menu
{
    public class NavigationMenu : Control
    {
        private ListView _listView;

        static NavigationMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationMenu), new FrameworkPropertyMetadata(typeof(NavigationMenu)));
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(IEnumerable<INavigationMenuDescriptor>),
            typeof(NavigationMenu),
            new PropertyMetadata(null, ItemsSourcePropertyChanged));

        public static readonly DependencyProperty MenuItemsProperty = DependencyProperty.Register(
            "MenuItems",
            typeof(ICollection<NavigationMenuItem>),
            typeof(NavigationMenu),
            new PropertyMetadata(null, MenuItemsPropertyChanged));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(INavigationMenuDescriptor),
            typeof(NavigationMenu),
            new FrameworkPropertyMetadata(null)
            {
                BindsTwoWayByDefault = true,
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

        public static readonly DependencyProperty SelectedMenuItemProperty = DependencyProperty.Register(
            "SelectedMenuItem",
            typeof(NavigationMenuItem),
            typeof(NavigationMenu),
            new PropertyMetadata(null, SelectedMenuItemPropertyChanged));

        private ItemsControl _menuItemsControl;

        public ICollection<NavigationMenuItem> MenuItems
        {
            set { SetValue(MenuItemsProperty, value); }
            get { return (ICollection<NavigationMenuItem>)GetValue(MenuItemsProperty); }
        }

        public NavigationMenuItem SelectedMenuItem
        {
            set { SetValue(SelectedMenuItemProperty, value); }
            get { return (NavigationMenuItem)GetValue(SelectedMenuItemProperty); }
        }

        public IEnumerable<INavigationMenuDescriptor> ItemsSource
        {
            set { SetValue(ItemsSourceProperty, value); }
            get { return (IEnumerable<INavigationMenuDescriptor>)GetValue(ItemsSourceProperty); }
        }

        public INavigationMenuDescriptor SelectedItem
        {
            set { SetValue(SelectedItemProperty, value); }
            get { return (INavigationMenuDescriptor)GetValue(SelectedItemProperty); }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            _menuItemsControl = Template.FindName("PART_MenuItems", this) as ItemsControl;
        }

        private void UnselectOldSelectedMenuItems()
        {
            var oldSelectedMenuItems = MenuItems.Where(item => item.MenuItems != null)
                .SelectMany(item => item.MenuItems)
                .Concat(MenuItems)
                .Where(item => !Equals(item, SelectedMenuItem))
                .Where(item => item.IsSelected)
                .ToArray();

            foreach (var item in oldSelectedMenuItems)
            {
                if (!(item.MenuItems?.Any(subItem => subItem.Equals(SelectedMenuItem)) ?? false))
                {
                    item.IsSelected = false;
                }
            }
        }

        private static void SelectedMenuItemPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {

        }

        private static void MenuItemsPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {

        }

        private static void ItemsSourcePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var control = (NavigationMenu)sender;
            control.SetupMenuItems(args.NewValue as IEnumerable<INavigationMenuDescriptor>);
        }

        private void SetupMenuItems(IEnumerable<INavigationMenuDescriptor> navigationMenuDescriptors)
        {
            var menuDescriptors = navigationMenuDescriptors?.ToArray();
            var menuItems = new List<NavigationMenuItem>();
            CalculateMenuItems(menuDescriptors, menuItems);
            MenuItems = menuItems;
            MenuItems?.FirstOrDefault()?.MarkAsSelected();
        }

        private void OnNavigationMenuItemSelectionChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {
            e.Handled = true;
            var selectedNavigationItem = sender as NavigationMenuItem;

            if (selectedNavigationItem != null && e.NewValue)
            {
                SelectedMenuItem = selectedNavigationItem;
                SelectedItem = selectedNavigationItem?.DataContext as INavigationMenuDescriptor;
                UnselectOldSelectedMenuItems();
            }
        }

        private void CalculateMenuItems(ICollection<INavigationMenuDescriptor> menuDescriptors, ICollection<NavigationMenuItem> menuItems)
        {
            if (menuDescriptors == null)
            {
                return;
            }

            foreach (var menuDescriptor in menuDescriptors)
            {
                var internalMenuItem = new NavigationMenuItem { DataContext = menuDescriptor };
                internalMenuItem.AddHandler(NavigationMenuItem.SelectionChangedEvent, new RoutedPropertyChangedEventHandler<bool>(OnNavigationMenuItemSelectionChanged));
                menuItems.Add(internalMenuItem);

                if (menuDescriptor.Items?.Any() ?? false)
                {
                    if (internalMenuItem.MenuItems == null)
                    {
                        internalMenuItem.MenuItems = new List<NavigationMenuItem>();
                    }

                    CalculateMenuItems(menuDescriptor.Items, internalMenuItem.MenuItems);
                }
            }
        }
    }
}
