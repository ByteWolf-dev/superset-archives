namespace Base.WpfMvvm;

using System.Windows;

public class BaseWindowNavigator : IBaseWindowNavigator
{
    private readonly Window _window;
    public           Window Window => _window;

    public BaseWindowNavigator(Window window)
    {
        _window = window;
    }

    public bool AskYesNoMessageBox(string caption, string text)
    {
        var answer = MessageBox.Show(text, caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Exclamation);
        return answer == MessageBoxResult.Yes;
    }

    public void CloseWindow()
    {
        _window.DialogResult = true;
        _window.Close();
    }
}