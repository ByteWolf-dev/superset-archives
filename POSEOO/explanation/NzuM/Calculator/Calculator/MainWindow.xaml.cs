using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private String _fistNumber = "";
    private String _operatorPressed = String.Empty;
    private String _secondNumber = "";

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Button? button = sender as Button;
        if (button != null)
        {
            if (_operatorPressed == String.Empty)
            {
                _fistNumber += button.Content.ToString();
                _result.Content = _fistNumber;
            }
            else
            {
                _secondNumber += button.Content.ToString();
                _result.Content = _secondNumber;
            }
        }
    }

    private void Operator_Click(object sender, RoutedEventArgs e)
    {
        Button? button = sender as Button;
        if (button != null)
        {
            switch (button.Content.ToString())
            {
                case "+":
                    _operatorPressed = "+";
                    break;
                case "-":
                    _operatorPressed = "-";
                    break;
                case "*":
                    _operatorPressed = "*";
                    break;
                case "/":
                    _operatorPressed = "/";
                    break;
                case "=":
                    Solve();
                    break;
            }
        }
    }

    private void Solve()
    {
        int result = 0;
        switch (_operatorPressed)
        {
            case "+":
                result = Int32.Parse(_fistNumber) + Int32.Parse(_secondNumber);
                break;
            case "-":
                result = Int32.Parse(_fistNumber) - Int32.Parse(_secondNumber);
                break;
            case "*":
                result = Int32.Parse(_fistNumber) * Int32.Parse(_secondNumber);
                break;
            case "/":
                result = Int32.Parse(_fistNumber) / Int32.Parse(_secondNumber);
                break;
        }

        _fistNumber = result.ToString();
        _secondNumber = "";
        _operatorPressed = "";
        _result.Content = result.ToString();
    }

    private void Clear(object sender, RoutedEventArgs e)
    {
        _fistNumber = "";
        _secondNumber = "";
        _operatorPressed = "";
        _result.Content = 0;
    }
}