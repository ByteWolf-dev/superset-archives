namespace WinUIWpf.Views;

using System.Windows;

using WinUIWpf.ViewModels;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as MainViewModel)!
                .LoadDataAsync();
        };
    }
}