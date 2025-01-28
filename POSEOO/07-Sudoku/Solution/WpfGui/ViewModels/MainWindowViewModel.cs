namespace Sudoku.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Solve.Abstraction;

using Sudoku.Models;
using Sudoku.Tools;

public class MainWindowViewModel : BaseViewModel
{
    #region crt

    public MainWindowViewModel(ISudokuSolveService solveService)
    {
        _solveService = solveService;

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                _cell[row, col] = new Cell()
                {
                    Row = row,
                    Col = col
                };
            }
        }
    }

    private readonly ISudokuSolveService _solveService;

    #endregion

    #region Properties

    private Cell[,] _cell = new Cell[9, 9];

    public Cell GetCell(int row, int col) => _cell[row, col];

    public int? SolutionCount
    {
        get => _solutionCount;
        set => SetProperty(ref _solutionCount, value);
    }

    public int? MoveCount
    {
        get => _moveCount;
        set => SetProperty(ref _moveCount, value);
    }

    private int? _solutionCount;
    private int? _moveCount;

    #endregion

    #region Commands

    public ICommand CellCommand   => new RelayCommand(async (parameter) => await NextValue(parameter), CanCellCommand);
    public ICommand NewCommand    => new RelayCommand(async (parameter) => await NewSudoku(parameter));
    public ICommand FinishCommand => new RelayCommand(async (parameter) => await FinishSudoku(parameter));
    public ICommand EnterCommand  => new RelayCommand(async (parameter) => await EnterSudoku(parameter));

    #endregion

    #region Operations

    private bool _inCellCommand = false;

    bool CanCellCommand(object? parameter)
    {
        return !_inCellCommand;
    }

    async Task NextValue(object? parameter)
    {
        _inCellCommand = true;

        try
        {
            if (parameter is not null)
            {
                await NextValue((Cell)parameter);
            }
        }
        finally
        {
            _inCellCommand = false;
            CommandManager.InvalidateRequerySuggested();
        }
    }

    async Task NewSudoku(object? parameter)
    {
        Sudoku = new List<string>();

        var ok = await StartCalc();
    }

    async Task FinishSudoku(object? parameter)
    {
        var result = await _solveService.FinishSudoku(Sudoku);

        if (result != null)
        {
            Sudoku = result.Sudoku;

            var ok = await StartCalc();
        }
    }

    async Task EnterSudoku(object? parameter)
    {
        Sudoku = Controller?.ShowEnterSudoku(Sudoku.ToList()) ?? Sudoku;
        var ok = await StartCalc();
    }

    #endregion

    #region Helpers

    private async Task NextValue(Cell cell)
    {
        await NextNo(cell.Row, cell.Col);
    }

    private IEnumerable<string> Sudoku { get; set; } = new List<string>();

    public async Task LoadDataAsync()
    {
        Sudoku = new List<string>
        {
            "1, ,8,5, , ,2,3,4",
            "5, , ,3, ,2,1,7,8",
            " , , ,8, , ,5,6,9",
            "8, , ,6, ,5,7,9,3",
            " , ,5,9, , ,4,8,1",
            "3, , , , ,8,6,5,2",
            "9,8, ,2, ,6,3,1, ",
            " , , , , , ,8, , ",
            " , , ,7,8, ,9, , ",
        };

        var ok = await StartCalc();
    }

    private void ApplyResults(SudokuSolveResult? sudokuResult)
    {
        if (sudokuResult != null)
        {
            int count = 0;

            foreach (var cellResult in sudokuResult!.Field)
            {
                var cell = _cell[cellResult.Row, cellResult.Col];
                if (cellResult.No.HasValue)
                {
                    count++;
                }

                cell.No                     = cellResult.No;
                cell.Possible               = cellResult.Possible;
                cell.AllPossible            = cellResult.AllPossible;
                cell.NotPossibleExplanation = cellResult.NotPossibleExplanation;
            }

            MoveCount = count;
        }
    }


    private async Task<bool> StartCalc()
    {
        SolutionCount = null;
        MoveCount     = null;

        var sudokuResult = await _solveService.Solve(Sudoku);

        ApplyResults(sudokuResult);

        SolutionCount = await _solveService.GetSolutionCount(Sudoku); // should not wait
        return true;
    }

    private async Task<bool> NextNo(int row, int col)
    {
        try
        {
            Sudoku = (await _solveService.NextNo(Sudoku, row, col)) ?? Sudoku;
            return await StartCalc();
        }
        catch (Exception)
        {
        }

        return false;
    }

    #endregion
}