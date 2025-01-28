namespace Sudoku.Tools;

using System.Collections.Generic;

public interface IWindowNavigator
{
    void CloseWindow();

    ICollection<string>? ShowEnterSudoku(ICollection<string> sudoku);
}