namespace WinUIWpf.Views;

using System.Windows;

using WinUIWpf.ViewModels;

public partial class ExamResultWindow : Window
{
    public ExamResultWindow(ExamResultViewModel viewModel)
    {
        InitializeComponent();
        viewModel.Controller = new WindowNavigator(this);
        DataContext          = viewModel;

        Loaded += async (v, e) =>
        {
            await (DataContext as ExamResultViewModel)!
                .LoadDataAsync();
        };
    }
}