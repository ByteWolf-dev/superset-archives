namespace Sudoku.Controls;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Sudoku.Models;

public partial class SudokuCell : UserControl
{
    #region ctr

    public SudokuCell()
    {
        InitializeComponent();
    }

    #endregion

    #region CommandProperty

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(SudokuCell), new PropertyMetadata(default(ICommand)));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    #endregion

    #region CommandParameterProperty

    public static DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object),
        typeof(SudokuCell), new PropertyMetadata("1", OnCommandParameterChanged));

    public object CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    private static void OnCommandParameterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (SudokuCell)dependencyObject;
        ctrl.InvalidateVisual();
    }

    #endregion

    #region ValueProperty

    public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Cell), typeof(SudokuCell), new PropertyMetadata(null, OnValueChanged));

    public Cell Value
    {
        get => (Cell)GetValue(ValueProperty);
        set
        {
            SetValue(ValueProperty, value);
            double LeftBorder(int   col) => (col % 3 == 0) ? 3.0 : 1.0;
            double RightBorder(int  col) => (col == 8) ? 3.0 : 0.0;
            double TopBorder(int    row) => (row % 3 == 0) ? 3.0 : 1.0;
            double BottomBorder(int row) => (row == 8) ? 3.0 : 0.0;
            BorderThickness = new(
                LeftBorder(value.Col),
                TopBorder(value.Row),
                RightBorder(value.Col),
                BottomBorder(value.Row));
        }
    }

    private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (SudokuCell)dependencyObject;
        ctrl.InvalidateVisual();
    }

    #endregion
}