namespace Sudoku.Views;

using System.Collections.Generic;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;

using Sudoku.Tools;
using Sudoku.ViewModels;

public class WindowNavigator : IWindowNavigator
{
    private readonly Window _window;

    public WindowNavigator(Window window)
    {
        _window = window;
    }

    public ICollection<string>? ShowEnterSudoku(ICollection<string> sudoku)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var enterSudokuWindow = scope.ServiceProvider.GetRequiredService<EnterSudokuWindow>();
            var dc                = (enterSudokuWindow.DataContext as EnterSudokuViewModel)!;
            dc.Sudoku = sudoku;
            if (enterSudokuWindow.ShowDialog() ?? false)
            {
                return dc.Sudoku;
            }
        }

        return null;
    }

    public void CloseWindow()
    {
        _window.DialogResult = true;
        _window.Close();
    }
}