namespace WinUIWpf.ViewModels;

using Core.Contracts;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.QueryResults;

public class ImportViewModel : BaseViewModel
{
    #region ctr

    public ImportViewModel(IUnitOfWork uow, IImportController importController)
    {
        _uow              = uow;
        _importController = importController;
    }

    private IUnitOfWork       _uow;
    private IImportController _importController;

    #endregion

    #region Properties

    public IWindowNavigator? Controller { get; set; }

    public ObservableCollection<CsvFile> CsvFiles { get; set; } = new ObservableCollection<CsvFile>();
    public CsvFile?                      Selected { get; set; }

    public RelayCommand ImportCommand    => new RelayCommand(async _ => await ImportFileAsync(),     (_) => Selected != null);
    public RelayCommand ImportAllCommand => new RelayCommand(async _ => await ImportAllFilesAsync(), (_) => CsvFiles.Count > 0);
    public RelayCommand CloseCommand     => new RelayCommand(_ => Controller?.CloseWindow());

    #endregion

    #region Operations

    public async Task LoadDataAsync()
    {
        CsvFiles.Clear();

        var files = await _importController.GetNotImportedFilesAsync();

        foreach (var v in files)
        {
            CsvFiles.Add(v);
        }
    }

    private async Task ImportFileAsync()
    {
        await _importController.ImportCsvFileAsync(Selected!);
        await LoadDataAsync();
    }

    private async Task ImportAllFilesAsync()
    {
        foreach (var file in CsvFiles)
        {
            await _importController.ImportCsvFileAsync(file);
        }

        await LoadDataAsync();
    }

    #endregion
}