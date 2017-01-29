using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ModernShell.Controls.Menu
{
    public class SmallNavigationMenuItem : Control
    {
        private bool _deferredSelection;
        private Popup _popup;
        private ItemsControl _menuItemsControl;
        
        static SmallNavigationMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SmallNavigationMenuItem), new FrameworkPropertyMetadata(typeof(SmallNavigationMenuItem)));
        }

        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent(
            "SelectionChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<bool>),
            typeof(SmallNavigationMenuItem));

        public static readonly DependencyProperty MenuItemsProperty = DependencyProperty.Register(
            "MenuItems",
            typeof(ICollection<SmallNavigationMenuItem>),
            typeof(SmallNavigationMenuItem),
            new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedMenuItemProperty = DependencyProperty.Register(
            "SelectedMenuItem",
            typeof(SmallNavigationMenuItem),
            typeof(SmallNavigationMenuItem),
            new PropertyMetadata(null));

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected",
            typeof(bool),
            typeof(SmallNavigationMenuItem),
            new PropertyMetadata(false, OnIsSelectedChanged));

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate",
            typeof(DataTemplate),
            typeof(SmallNavigationMenuItem),
            new PropertyMetadata(null));
        
        public event RoutedPropertyChangedEventHandler<bool> SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        public ICollection<SmallNavigationMenuItem> MenuItems
        {
            set { SetValue(MenuItemsProperty, value); }
            get { return (ICollection<SmallNavigationMenuItem>)GetValue(MenuItemsProperty); }
        }

        public SmallNavigationMenuItem SelectedMenuItem
        {
            set { SetValue(SelectedMenuItemProperty, value); }
            get { return (SmallNavigationMenuItem)GetValue(SelectedMenuItemProperty); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            set { SetValue(ItemTemplateProperty, value); }
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _menuItemsControl = Template.FindName("PART_MenuItems", this) as ItemsControl;
            _popup = Template.FindName("PART_Popup", this) as Popup;

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
            var item = (SmallNavigationMenuItem)d;
            //item._popup.IsOpen = false;
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if ((MenuItems?.Any() ?? false))
            {
                _popup.IsOpen = !_popup.IsOpen;
            }

            if (!IsSelected)
            {
                SelectItem();
            }

            e.Handled = true;
        }

        internal void ClosePopup()
        {
            _popup.IsOpen = false;
        }

        internal void SelectItem()
        {
            var oldSelectionValue = IsSelected;
            IsSelected = !IsSelected;
            RaiseEvent(new RoutedPropertyChangedEventArgs<bool>(oldSelectionValue, IsSelected, SelectionChangedEvent));
        }
    }
}