using System;
using System.Windows;

namespace Sudoku;

using Microsoft.Extensions.DependencyInjection;

using Solve;
using Solve.Abstraction;

using Sudoku.Views;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void AppStartup(object sender, StartupEventArgs e)
    {
        AppService.ServiceCollection = new ServiceCollection();
        AppService.ServiceCollection
            .AddAssemblyByName(
                n => n.EndsWith("ViewModel"),
                ServiceLifetime.Transient,
                typeof(ViewModels.MainWindowViewModel).Assembly)
            .AddTransient<MainWindow>()
            .AddTransient<EnterSudokuWindow>()
            .AddTransient<ISudokuSolveService, SudokuSolveService>();

        AppService.ServiceCollection.AddHttpClient("Azure",    httpClient => { httpClient.BaseAddress = new Uri("https://sudokusolve.azurewebsites.net/api/"); });
        AppService.ServiceCollection.AddHttpClient("local",    httpClient => { httpClient.BaseAddress = new Uri("http://localhost:5185/api/"); });
        AppService.ServiceCollection.AddHttpClient("pi",       httpClient => { httpClient.BaseAddress = new Uri("https://ait.dyndns-home.com/sudokusolve/api/"); });
        AppService.ServiceCollection.AddHttpClient("leocloud", httpClient => { httpClient.BaseAddress = new Uri("https://h-aitenbichler.cloud.htl-leonding.ac.at/sudokusolve/api/"); });

        AppService.BuildServiceProvider();

        MainWindow = AppService.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }
}