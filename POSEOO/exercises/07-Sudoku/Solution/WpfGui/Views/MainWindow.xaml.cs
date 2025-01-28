namespace Sudoku.Views;

using System.Windows;
using System.Windows.Controls;

using Sudoku.Controls;
using Sudoku.Tools;
using Sudoku.ViewModels;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel vm)
    {
        vm.Controller = new WindowNavigator(this);

        InitializeComponent();

        DataContext = vm;
        
        for (int row = 0; row < 9; row++)
        {
            var c = new Label
            {
                Content = row+1,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            c.SetValue(Grid.RowProperty,    row + 1);
            c.SetValue(Grid.ColumnProperty, 0);

            SudokuGrid.Children.Add(c);
        }

        for (int col = 0; col < 9; col++)
        {
            var c = new Label
            {
                Content                    = (char) (col + 'A'),
                HorizontalContentAlignment = HorizontalAlignment.Center
            };
            c.SetValue(Grid.RowProperty,    0);
            c.SetValue(Grid.ColumnProperty, col+1);

            SudokuGrid.Children.Add(c);
        }


        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                var c = new SudokuCell
                {
                    Command          = vm.CellCommand,
                    CommandParameter = vm.GetCell(row, col),
                    Value            = vm.GetCell(row, col)
                };
                c.SetValue(Grid.RowProperty,    row+1);
                c.SetValue(Grid.ColumnProperty, col+1);

                SudokuGrid.Children.Add(c);
            }
        }

        Loaded += async (v, e) =>
        {
            await (DataContext as MainWindowViewModel)!
                .LoadDataAsync();
        };
    }
}