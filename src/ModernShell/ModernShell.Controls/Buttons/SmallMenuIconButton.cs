using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ModernShell.Controls.Buttons
{
    public class SmallMenuIconButton : Control
    {
        private ContentPresenter _button;
        private Popup _popup;

        public static readonly DependencyProperty ButtonTemplateProperty = DependencyProperty.Register(
            "ButtonTemplate",
            typeof(DataTemplate),
            typeof(SmallMenuIconButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MenuItemTemplateProperty = DependencyProperty.Register(
            "MenuItemTemplate",
            typeof(DataTemplate),
            typeof(SmallMenuIconButton),
            new PropertyMetadata(null));

        public static readonly DependencyProperty MenuItemsProperty = DependencyProperty.Register(
            "MenuItems",
            typeof(IEnumerable),
            typeof(SmallMenuIconButton),
            new PropertyMetadata(null));

        static SmallMenuIconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SmallMenuIconButton), new FrameworkPropertyMetadata(typeof(SmallMenuIconButton)));
        }

        public DataTemplate MenuItemTemplate
        {
            set { SetValue(MenuItemTemplateProperty, value); }
            get { return (DataTemplate)GetValue(MenuItemTemplateProperty); }
        }

        public DataTemplate ButtonTemplate
        {
            set { SetValue(ButtonTemplateProperty, value); }
            get { return (DataTemplate)GetValue(ButtonTemplateProperty); }
        }

        public IEnumerable MenuItems
        {
            set { SetValue(MenuItemsProperty, value); }
            get { return (IEnumerable)GetValue(MenuItemsProperty); }
        }

        public bool HasMenuItems => (MenuItems?.OfType<object>())?.Any() ?? false;

        public override void OnApplyTemplate()
        {
            _button = GetTemplateChild("PART_Button") as ContentPresenter;
            _popup = GetTemplateChild("PART_Popup") as Popup;

            SetupButton();
            SetupPopup();
        }

        private void SetupButton()
        {
            _button.MouseEnter += OnButtonMouseEnter;
            _button.MouseLeftButtonDown += OnButtonMouseLeftDown;
        }

        private void OnButtonMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (HasMenuItems)
            {
                _popup.IsOpen = !_popup.IsOpen;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            _popup.IsOpen = false;
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            _popup.IsOpen = false;
            base.OnMouseLeftButtonUp(e);
        }

        private void SetupPopup()
        {
            
        }

        private void OnButtonMouseEnter(object sender, MouseEventArgs e)
        {
            if (HasMenuItems)
            {
                _popup.IsOpen = true;
            }
        }
    }
}