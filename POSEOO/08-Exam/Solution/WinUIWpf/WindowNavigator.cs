namespace WinUIWpf;

using System.Windows;

using Base.Core;
using Base.WpfMvvm;

using Microsoft.Extensions.DependencyInjection;

using WinUIWpf.ViewModels;
using WinUIWpf.Views;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }

    public void ShowImportWindow()
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var importWindow = scope.ServiceProvider.GetRequiredService<ImportWindow>();
            importWindow.ShowDialog();
        }
    }

    public void ShowExamResultWindow(int examId)
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var examResultWindow = scope.ServiceProvider.GetRequiredService<ExamResultWindow>();
            var examResultVm     = examResultWindow.DataContext as ExamResultViewModel;
            examResultVm.ExamId = examId;
            examResultWindow.ShowDialog();
        }
    }
}