using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ModernShell.Controls.Panels
{
    public class ProgressPanel : Control
    {
        private Rectangle _progressBackgroundLine;
        private Rectangle _progressLine;
        private AnimatedTextBlock _progressText;

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header",
            typeof(string),
            typeof(ProgressPanel),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(ProgressPanel),
            new PropertyMetadata(default(double), OnValueChanged));

        public static readonly DependencyProperty ValueFormatProperty = DependencyProperty.Register(
            "ValueFormat",
            typeof(string),
            typeof(ProgressPanel),
            new PropertyMetadata("{0:N2}"));

        public static readonly DependencyProperty DifferenceProperty = DependencyProperty.Register(
            "Difference",
            typeof(double),
            typeof(ProgressPanel),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(
            "MaxValue",
            typeof(double),
            typeof(ProgressPanel),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty AnimationLengthProperty = DependencyProperty.Register(
            "AnimationLength",
            typeof(int),
            typeof(ProgressPanel),
            new PropertyMetadata(default(int)));

        static ProgressPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressPanel), new FrameworkPropertyMetadata(typeof(ProgressPanel)));
        }

        public ProgressPanel()
        {
            Loaded += OnProgressPanelLoaded;
        }

        public double MaxValue
        {
            set { SetValue(MaxValueProperty, value); }
            get { return (double)GetValue(MaxValueProperty); }
        }

        public double Value
        {
            set { SetValue(ValueProperty, value); }
            get { return (double)GetValue(ValueProperty); }
        }

        public string ValueFormat
        {
            set { SetValue(ValueFormatProperty, value); }
            get { return (string)GetValue(ValueFormatProperty); }
        }

        public string Header
        {
            set { SetValue(HeaderProperty, value); }
            get { return (string)GetValue(HeaderProperty); }
        }

        public int AnimationLength
        {
            set { SetValue(AnimationLengthProperty, value); }
            get { return (int)GetValue(AnimationLengthProperty); }
        }

        public double Difference
        {
            set { SetValue(DifferenceProperty, value); }
            get { return (double)GetValue(DifferenceProperty); }
        }

        public Storyboard TransitionStoryboard => new Storyboard();

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _progressText = Template.FindName("ProgressText", this) as AnimatedTextBlock;
            _progressLine = Template.FindName("ProgressLine", this) as Rectangle;
            _progressBackgroundLine = Template.FindName("ProgressLineBackground", this) as Rectangle;
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = (ProgressPanel)d;
            panel.DoTransition();
        }

        private void OnProgressPanelLoaded(object sender, RoutedEventArgs e)
        {
            DoTransition();
        }

        private void DoTransition()
        {
            if (!IsLoaded || Math.Abs(MaxValue - default(double)) < double.Epsilon)
            {
                return;
            }

            double progress = Value / MaxValue;
            double progressTargetWidth = _progressBackgroundLine.ActualWidth * progress;
            IEasingFunction easing = new SineEase { EasingMode = EasingMode.EaseIn };
            
            var widthAnimation = new DoubleAnimation
            {
                From = 0d,
                To = progressTargetWidth,
                Duration = TimeSpan.FromMilliseconds(AnimationLength),
                EasingFunction = easing,
            };

            var valueAnimation = new DoubleAnimation
            {
                From = 0d,
                To = Value,
                Duration = TimeSpan.FromMilliseconds(AnimationLength),
                EasingFunction = easing,
            };
            
            _progressLine.BeginAnimation(Rectangle.WidthProperty, widthAnimation);
            _progressText.BeginAnimation(AnimatedTextBlock.ValueProperty, valueAnimation);
        }
    }
}
