using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ModernShell.Controls.Menu
{
    public class NavigationMenu : Control
    {
        static NavigationMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationMenu), new FrameworkPropertyMetadata(typeof(NavigationMenu)));
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(ICollection<INavigationMenuDescriptor>),
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
            new PropertyMetadata(null, SelectedItemPropertyChanged));

        public static readonly DependencyProperty SelectedMenuItemProperty = DependencyProperty.Register(
            "SelectedMenuItem",
            typeof(NavigationMenuItem),
            typeof(NavigationMenu),
            new PropertyMetadata(null, SelectedMenuItemPropertyChanged));

        private TreeView _treeView
            ;

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

        public ICollection<INavigationMenuDescriptor> ItemsSource
        {
            set { SetValue(ItemsSourceProperty, value); }
            get { return (ICollection<INavigationMenuDescriptor>)GetValue(ItemsSourceProperty); }
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
            _treeView = FindName("PART_TreeView") as TreeView;

            if (_treeView != null)
            {
                _treeView.SelectedItemChanged += OnTreeViewSelectedItemChanged;
            }
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var menuItem = e.NewValue as NavigationMenuItem;

            SelectedMenuItem = menuItem;
            SelectedItem = menuItem?.DataContext as INavigationMenuDescriptor;

        }

        private static void SelectedMenuItemPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {

        }


        private static void SelectedItemPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {

        }



        private static void MenuItemsPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {

        }



        private static void ItemsSourcePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var control = (NavigationMenu)sender;
            control.SetupMenuItems(args.NewValue as ICollection<INavigationMenuDescriptor>);
        }

        private void SetupMenuItems(ICollection<INavigationMenuDescriptor> navigationMenuDescriptors)
        {
            var menuItems = new List<NavigationMenuItem>();
            CalculateMenuItems(navigationMenuDescriptors, menuItems, null);
            MenuItems = menuItems;
        }

        private static void CalculateMenuItems(ICollection<INavigationMenuDescriptor> menuDescriptors, ICollection<NavigationMenuItem> menuItems, NavigationMenuItem menuItem)
        {
            if (menuDescriptors == null)
            {
                return;
            }

            foreach (var menuDescriptor in menuDescriptors)
            {
                var internalMenuItem = menuItem ?? new NavigationMenuItem { DataContext = menuDescriptors };
                menuItems.Add(internalMenuItem);

                if (menuDescriptor.Items?.Any() ?? false)
                {
                    CalculateMenuItems(menuDescriptors, menuItems, internalMenuItem);
                }
            }
        }
    }
}
