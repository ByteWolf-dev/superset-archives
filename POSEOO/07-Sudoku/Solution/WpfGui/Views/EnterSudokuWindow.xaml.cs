namespace Sudoku.Views;

using System.Windows;

using Sudoku.ViewModels;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class EnterSudokuWindow : Window
{
    public EnterSudokuWindow(EnterSudokuViewModel vm)
    {
        vm.Controller = new WindowNavigator(this);
        InitializeComponent();

        DataContext = vm;

        Loaded += async (v, e) =>
        {
            await (DataContext as EnterSudokuViewModel)!
                .LoadDataAsync();
        };
    }
}