namespace Solve;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Solve.Abstraction;

public class SudokuSolveService : ISudokuSolveService
{
    public SudokuSolveService(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    private readonly IHttpClientFactory _httpFactory;

    public HttpClient Http => _httpFactory.CreateClient("leocloud");


    private string ToFirstQuery(IEnumerable<string> sudoku)
    {
        return "?sudoku=" + string.Join("&sudoku=", sudoku);
    }

    public async Task<SudokuSolveResult?> Solve(IEnumerable<string> sudoku)
    {
        try
        {
            return await Http.GetFromJsonAsync<SudokuSolveResult>("Sudoku" + ToFirstQuery(sudoku));
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<int> GetSolutionCount(IEnumerable<string> sudoku)
    {
        try
        {
            var query         = ToFirstQuery(sudoku);
            var solutionCount = await Http.GetFromJsonAsync<int>("Sudoku/solutioncount" + query);
            return solutionCount;
        }
        catch (Exception)
        {
        }

        return -1;
    }

    public async Task<IEnumerable<string>?> NextNo(IEnumerable<string> sudoku, int row, int col)
    {
        try
        {
            var query = ToFirstQuery(sudoku);
            query += $"&row={row}&col={col}";
            return (await Http.GetFromJsonAsync<IEnumerable<string>>("Sudoku/next" + query))!;
        }
        catch (Exception)
        {
        }

        return null;
    }

    public async Task<SudokuResult?> FinishSudoku(IEnumerable<string> sudoku)
    {
        try
        {
            return await Http.GetFromJsonAsync<SudokuResult>("Sudoku/finish" + ToFirstQuery(sudoku));
        }
        catch (Exception)
        {
            return null;
        }
    }
}