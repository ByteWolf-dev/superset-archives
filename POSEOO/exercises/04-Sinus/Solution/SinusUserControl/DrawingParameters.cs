using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System.Windows.Input;

namespace SinusUserControl;

public class DrawingParameters : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
    {
        var memberExpression = (MemberExpression)projection.Body;
        OnPropertyChanged(memberExpression.Member.Name);
    }

    private double _scaleX = 10;
    public double ScaleX
    {
        get => _scaleX;
        set { _scaleX = value; OnPropertyChanged(); }
    }

    private double _scaleY = 10;
    public double ScaleY
    {
        get => _scaleY;
        set { _scaleY = value; OnPropertyChanged(); }
    }

    private double _offsetX = 10;
    public double OffsetX
    {
        get => _offsetX;
        set { _offsetX = value; OnPropertyChanged(); }
    }

    private double _offsetY = 10;
    public double OffsetY
    {
        get => _offsetY;
        set { _offsetY = value; OnPropertyChanged(); }
    }

    double offsetInc = Math.PI * 2 / 5;

    public ICommand OffsetXPlus  => new DelegateCommand(() => OffsetX = OffsetX + offsetInc);
    public ICommand OffsetXMinus => new DelegateCommand(() => OffsetX = OffsetX - offsetInc);
    public ICommand OffsetYPlus  => new DelegateCommand(() => OffsetY = OffsetY + offsetInc);
    public ICommand OffsetYMinus => new DelegateCommand(() => OffsetY = OffsetY - offsetInc);

    public ICommand ScaleXPlus  => new DelegateCommand(() => ScaleX = ScaleX * 1.1);
    public ICommand ScaleXMinus => new DelegateCommand(() => ScaleX = ScaleX * 0.9);
    public ICommand ScaleYPlus  => new DelegateCommand(() => ScaleY = ScaleY * 1.1);
    public ICommand ScaleYMinus => new DelegateCommand(() => ScaleY = ScaleY * 0.9);

}