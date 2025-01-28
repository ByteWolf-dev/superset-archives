using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SinusUserControl;

/// <summary>
/// Interaction logic for SinusControl.xaml
/// </summary>
public partial class SinusControl : UserControl
{
    public SinusControl()
    {   
            
        ScaleX  = 10.0;
        ScaleY  = 10;
        OffsetX = 0;
        OffsetY = 0;
        InitializeComponent();

    }

    public static DependencyProperty ScaleXProperty = DependencyProperty.Register(nameof(ScaleX), typeof(double), typeof(SinusControl), new PropertyMetadata(10.0, OnScaleXChanged));
    public double ScaleX
    {
        get => (double)GetValue(ScaleXProperty);
        set => SetValue(ScaleXProperty, value);
    }
    private static void OnScaleXChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (SinusControl)dependencyObject;
        ctrl.InvalidateVisual();
    }

    ///

    public static DependencyProperty ScaleYProperty = DependencyProperty.Register(nameof(ScaleY), typeof(double), typeof(SinusControl), new PropertyMetadata(10.0, OnScaleYChanged));

    public double ScaleY
    {
        get => (double)GetValue(ScaleYProperty);
        set => SetValue(ScaleYProperty, value);
    }
    private static void OnScaleYChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (SinusControl)dependencyObject;
        ctrl.InvalidateVisual();
    }

    /// 

    public static DependencyProperty OffsetXProperty = DependencyProperty.Register(nameof(OffsetX), typeof(double), typeof(SinusControl), new PropertyMetadata(10.0, OnOffsetXChanged));
    public double OffsetX
    {
        get => (double)GetValue(OffsetXProperty);
        set => SetValue(OffsetXProperty, value);
    }
    private static void OnOffsetXChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (SinusControl)dependencyObject;
        ctrl.InvalidateVisual();
    }

    ///

    public static DependencyProperty OffsetYProperty = DependencyProperty.Register(nameof(OffsetY), typeof(double), typeof(SinusControl), new PropertyMetadata(10.0, OnOffsetYChanged));

    public double OffsetY
    {
        get => (double)GetValue(OffsetYProperty);
        set => SetValue(OffsetYProperty, value);
    }
    private static void OnOffsetYChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (SinusControl)dependencyObject;
        ctrl.InvalidateVisual();
    }

    private static readonly Pen RedPen  = new Pen(new SolidColorBrush(Colors.Red),  1.0d);
    private static readonly Pen GridPen = new Pen(new SolidColorBrush(Colors.Blue), 0.5d);


    protected override void OnRender(DrawingContext drawingContext)
    {
        DrawSinus(drawingContext);
    }

    double ToX(double x) { return (x + OffsetX) * ScaleX ; }
    double ToY(double y) { return (ActualHeight / 2) - (y+ OffsetY) * ScaleY ; }

    void ToPoint(double x, double y, ref Point pt)
    {
        pt.X = ToX(x);
        pt.Y = ToY(y);
    }
    Point ToPoint(double x, double y)
    {
        Point pt = new Point();
        pt.X = ToX(x);
        pt.Y = ToY(y);
        return pt;
    }

    private void DrawSinus(DrawingContext context)
    {
        if (ScaleX == 0.0 || ScaleY == 0.0) return;


        context.DrawLine(GridPen, ToPoint(0, 0), ToPoint(Math.PI*2, 0));


        double x, y;
        double step = Math.PI*2 / 360; // draw 1° 

        Point ptold = new Point();
        Point ptnew = new Point();

        for (x=0.0; x < Math.PI * 2; x+=step)
        {
            y = Math.Sin(x);

            ToPoint(x, y, ref ptnew);

            if (x != 0)
            {
                context.DrawLine(RedPen, ptold, ptnew);
            }

            ptold.X = ptnew.X;
            ptold.Y = ptnew.Y;

            if (ptnew.X > ActualWidth)
                break;
        }
    }
}