namespace Solve.Abstraction;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISudokuSolveService
{
    Task<SudokuSolveResult?>   Solve(IEnumerable<string>            sudoku);
    Task<int>                  GetSolutionCount(IEnumerable<string> sudoku);
    Task<SudokuResult?>        FinishSudoku(IEnumerable<string>     sudoku);
    Task<IEnumerable<string>?> NextNo(IEnumerable<string>           sudoku, int row, int col);
}