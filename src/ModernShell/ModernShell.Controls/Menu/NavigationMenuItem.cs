using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModernShell.Controls.Menu
{
    public class NavigationMenuItem : Control
    {
        private ItemsControl _menuItemsControl;
        private bool _deferredSelection;

        static NavigationMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationMenuItem), new FrameworkPropertyMetadata(typeof(NavigationMenuItem)));
        }

        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectionChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<bool>),
            typeof(NavigationMenuItem));

        public static readonly DependencyProperty MenuItemsProperty = DependencyProperty.Register(
            "MenuItems",
            typeof(ICollection<NavigationMenuItem>),
            typeof(NavigationMenuItem),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedMenuItemProperty = DependencyProperty.Register(
            "SelectedMenuItem",
            typeof(NavigationMenuItem),
            typeof(NavigationMenuItem),
            new PropertyMetadata(null));

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected",
            typeof(bool),
            typeof(NavigationMenuItem),
            new PropertyMetadata(false, OnIsSelectedChanged));

        public event RoutedPropertyChangedEventHandler<bool> SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

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

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _menuItemsControl = Template.FindName("PART_MenuItems", this) as ItemsControl;

            if (_menuItemsControl != null && _deferredSelection)
            {
                _deferredSelection = false;
                SelectItem();
            }
        }

        public void MarkAsSelected()
        {
            if (_menuItemsControl == null)
            {
                _deferredSelection = true;
            }
            else
            {
                SelectItem();
            }
        }

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var item = (NavigationMenuItem)d;
            item.PostIsSelectedAction();
        }

        private void PostIsSelectedAction()
        {
            _menuItemsControl.Visibility = IsSelected && (MenuItems?.Any() ?? false)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (!IsSelected)
            {
                SelectItem();
            }

            e.Handled = true;
        }

        private void SelectItem()
        {
            var oldSelectionValue = IsSelected;
            IsSelected = !IsSelected;
            RaiseEvent(new RoutedPropertyChangedEventArgs<bool>(oldSelectionValue, IsSelected, SelectionChangedEvent));
        }
    }
}