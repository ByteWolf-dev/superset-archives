namespace WinUIWpf.Views;

using System.Windows;

using WinUIWpf.ViewModels;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class ImportWindow : Window
{
    public ImportWindow(ImportViewModel viewModel)
    {
        InitializeComponent();
        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as ImportViewModel)!
                .LoadDataAsync();
        };
    }
}