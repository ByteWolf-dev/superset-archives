namespace Base.WpfMvvm;

public interface IBaseWindowNavigator
{
    void CloseWindow();

    bool AskYesNoMessageBox(string caption, string text);
}