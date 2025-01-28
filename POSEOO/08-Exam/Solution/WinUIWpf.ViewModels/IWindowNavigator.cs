namespace WinUIWpf.ViewModels;

using Base.WpfMvvm;

public interface IWindowNavigator : IBaseWindowNavigator
{
    public void ShowImportWindow();

    public void ShowExamResultWindow(int examId);
}