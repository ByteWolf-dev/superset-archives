namespace WinUIWpf.ViewModels;

using Core.Contracts;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.Entities;
using Core.QueryResults;

public class ExamResultViewModel : BaseViewModel
{
    #region ctr

    public ExamResultViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    private IUnitOfWork _uow;

    #endregion

    #region Properties

    public IWindowNavigator? Controller { get; set; }

    public int ExamId { get; set; }

    public ObservableCollection<ExamResult> ExamResult { get; set; } = new ObservableCollection<ExamResult>();


    public RelayCommand CloseCommand => new RelayCommand(_ => Controller?.CloseWindow());

    #endregion

    #region Operations

    public async Task LoadDataAsync()
    {
        var exam = await _uow.Exam.GetByIdAsync(ExamId, nameof(Exam.ExamQuestions));

        ExamResult.Clear();

        var examResults = await _uow.Exam.GetExamResultAsync(ExamId);

        foreach (var v in examResults)
        {
            ExamResult.Add(v);
        }
    }

    #endregion
}