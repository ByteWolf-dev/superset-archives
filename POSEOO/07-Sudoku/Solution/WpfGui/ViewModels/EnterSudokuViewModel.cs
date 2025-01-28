namespace Sudoku.ViewModels;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Sudoku.Tools;

public class EnterSudokuViewModel : BaseViewModel
{
    #region crt

    public EnterSudokuViewModel()
    {
    }

    #endregion

    #region Properties

    public ICollection<string>? Sudoku { get; set; }

    public string? SudokuText
    {
        get => string.Join("\n", Sudoku!.Select(NormalizeLine));
        set => Sudoku = value!.Replace("\r", "")!.Split("\n");
    }

    #endregion

    #region Commands

    public ICommand ApplyCommand => new RelayCommand(async (parameter) => await Apply(parameter));

    #endregion

    #region Operations

    private string NormalizeLine(string line)
    {
        return line;
    }

    public async Task LoadDataAsync()
    {
        await Task.CompletedTask;
    }


    private async Task Apply(object? parameter)
    {
        await Task.CompletedTask;
        Controller!.CloseWindow();
    }

    #endregion
}