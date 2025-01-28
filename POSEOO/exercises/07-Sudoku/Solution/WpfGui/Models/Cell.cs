namespace Sudoku.Models;

using System;
using System.Collections.Generic;
using System.Linq;

using Sudoku.Tools;

public class Cell : NotifyPropertyChanged
{
    #region Sudoku Properties

    public string Text
    {
        get
        {
            if (HasNo)
            {
                return No!.Value.ToString();
            }

            var possible       = Possible ?? Array.Empty<int>();
            var allPossible    = AllPossible ?? Array.Empty<int>();
            var possibleStr    = string.Join(' ', possible);
            var notPossibleStr = string.Join(' ', allPossible.Except(possible));

            return string.IsNullOrEmpty(notPossibleStr) ? possibleStr : $"{possibleStr} - {notPossibleStr}";
        }
    }

    public string NotPossibleExplanationText => HasNo || NotPossibleExplanation == null || NotPossibleExplanation?.Count == 0 ? string.Empty : string.Join("\n", NotPossibleExplanation!);

    private int? _no = null;

    public int? No
    {
        get => _no;
        set
        {
            if (SetProperty(ref _no, value))
            {
                OnPropertyChangedCalculated();
            }
        }
    }

    private ICollection<int>? _possible = null;

    public ICollection<int>? Possible
    {
        get => _possible;
        set
        {
            if (SetProperty(ref _possible, value))
            {
                OnPropertyChangedCalculated();
            }
        }
    }

    private ICollection<int>? _allPossible = null;

    public ICollection<int>? AllPossible
    {
        get => _allPossible;
        set
        {
            if (SetProperty(ref _allPossible, value))
            {
                OnPropertyChangedCalculated();
            }
        }
    }

    private ICollection<string>? _notPossibleExplanation = null;
    public ICollection<string>? NotPossibleExplanation
    {
        get => _notPossibleExplanation;
        set
        {
            if (SetProperty(ref _notPossibleExplanation, value))
            {
                OnPropertyChangedCalculated();
            }
        }
    }

    public bool HasNo           => No.HasValue;
    public bool IsOnePossible   => !HasNo && Possible?.Count == 1;
    public bool AreManyPossible => !HasNo && Possible?.Count > 1;

    private void OnPropertyChangedCalculated()
    {
        OnPropertyChanged(nameof(Text));
        OnPropertyChanged(nameof(NotPossibleExplanationText));
        OnPropertyChanged(nameof(HasNo));
        OnPropertyChanged(nameof(IsOnePossible));
        OnPropertyChanged(nameof(AreManyPossible));
    }

    #endregion

    #region Row/Col Property

    private int _row;

    public int Row
    {
        get => _row;
        set => SetProperty(ref _row, value);
    }

    private int _col;

    public int Col
    {
        get => _col;
        set => SetProperty(ref _col, value);
    }

    #endregion
}