namespace WinUIWpf.ViewModels;

using Core.Contracts;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.Entities;

public class MainViewModel : BaseViewModel
{
    #region ctr

    public MainViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    private IUnitOfWork _uow;

    #endregion

    #region Properties

    public IWindowNavigator? Controller { get; set; }

    public ObservableCollection<Exam> Exams        { get; set; } = new ObservableCollection<Exam>();
    public Exam?                      SelectedExam { get; set; }

    public RelayCommand ShowExamResultCommand  => new RelayCommand(async _ => await ShowExamResult(), (_) => SelectedExam != null);
    public RelayCommand ImportWindowCommand => new RelayCommand(async _ => await OpenImportWindow());

    #endregion

    #region Operations

    public async Task LoadDataAsync()
    {
        Exams.Clear();
        var exams = await _uow.Exam.GetNoTrackingAsync();
        foreach (var v in exams)
        {
            Exams.Add(v);
        }
    }

    private async Task ShowExamResult()
    {
        await Task.CompletedTask;
        Controller?.ShowExamResultWindow(SelectedExam!.Id);
    }

    private async Task OpenImportWindow()
    {
        Controller?.ShowImportWindow();
        await LoadDataAsync();
    }

    #endregion
}