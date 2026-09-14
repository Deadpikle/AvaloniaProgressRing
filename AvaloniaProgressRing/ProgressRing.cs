using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaloniaProgressRing
{
    public class ProgressRing : TemplatedControl
    {
        private const string LargeState = ":large";
        private const string SmallState = ":small";

        private const string InactiveState = ":inactive";
        private const string ActiveState = ":active";

        private double _maxSideLength = 10;

        static ProgressRing()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressRing),
            //    new FrameworkPropertyMetadata(typeof(ProgressRing)));
        }

        public ProgressRing()
        {
        }

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public static readonly StyledProperty<bool> IsActiveProperty =
            AvaloniaProperty.Register<ProgressRing, bool>(
                nameof(IsActive), 
                defaultValue: true);

        private static void OnIsActiveChanged(AvaloniaObject obj, bool arg2)
        {
            ((ProgressRing)obj).UpdateVisualStates();
        }

        public static readonly DirectProperty<ProgressRing, double> MaxSideLengthProperty =
            AvaloniaProperty.RegisterDirect<ProgressRing, double>(
               nameof(MaxSideLength),
               o => o.MaxSideLength);

        public double MaxSideLength
        {
            get { return _maxSideLength; }
            private set { SetAndRaise(MaxSideLengthProperty, ref _maxSideLength, value); }
        }

        public static readonly StyledProperty<double> EllipseDiameterProperty =
            AvaloniaProperty.Register<ProgressRing, double>(
               nameof(EllipseDiameter),
               defaultValue: 10);

        public double EllipseDiameter
        {
            get => GetValue(EllipseDiameterProperty);
            set { SetValue(EllipseDiameterProperty, value); }
        }

        public static readonly StyledProperty<Thickness> EllipseOffsetProperty =
            AvaloniaProperty.Register<ProgressRing, Thickness>(
               nameof(EllipseOffset),
               defaultValue: new Thickness(2));

        public Thickness EllipseOffset
        {
            get { return GetValue(EllipseOffsetProperty); }
            set { SetValue(EllipseOffsetProperty, value); }
        }

        public bool ManuallyManageSizing
        {
            get => GetValue(ManuallyManageSizingProperty);
            set => SetValue(ManuallyManageSizingProperty, value);
        }

        public static readonly StyledProperty<bool> ManuallyManageSizingProperty =
            AvaloniaProperty.Register<ProgressRing, bool>(
                nameof(ManuallyManageSizing), 
                defaultValue: false);

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            MaxSideLength = Math.Max(Width, Height);
            if (!ManuallyManageSizing)
            {
                double ellipseDiameter = 0.1 * MaxSideLength;
                if (MaxSideLength <= 40)
                {
                    ellipseDiameter += 1;
                }

                EllipseDiameter = ellipseDiameter;
                EllipseOffset = new Thickness(0, MaxSideLength / 2 - ellipseDiameter, 0, 0);
            }
            UpdateVisualStates();
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == IsActiveProperty ||
                change.Property == EllipseDiameterProperty ||
                change.Property == EllipseOffsetProperty ||
                change.Property == ManuallyManageSizingProperty)
            {
                UpdateVisualStates();
            }
        }

        private void UpdateVisualStates()
        {
            PseudoClasses.Remove(ActiveState);
            PseudoClasses.Remove(InactiveState);
            PseudoClasses.Remove(SmallState);
            PseudoClasses.Remove(LargeState);
            PseudoClasses.Add(IsActive ? ActiveState : InactiveState);
            PseudoClasses.Add(_maxSideLength < 60 ? SmallState : LargeState);
        }
    }
}
