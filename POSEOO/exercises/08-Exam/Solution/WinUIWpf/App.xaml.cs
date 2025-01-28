namespace WinUIWpf;

using Core.Contracts;

using Microsoft.Extensions.DependencyInjection;

using Persistence;

using System.Windows;

using WinUIWpf.Views;

using Base.Core;
using Base.Tools;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void AppStartup(object sender, StartupEventArgs e)
    {
        var configuration = ConfigurationHelper.GetConfiguration();

        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        AppService.ServiceCollection = new ServiceCollection();
        AppService.ServiceCollection
            .AddSingleton(configuration)
            .AddAssemblyByName(
                n => n.EndsWith("ViewModel"),
                ServiceLifetime.Transient,
                typeof(ViewModels.MainViewModel).Assembly)
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddTransient<IExamineeRepository, ExamineeRepository>()
            .AddTransient<IExamRepository, ExamRepository>()
            .AddTransient<IExamQuestionRepository, ExamQuestionRepository>()
            .AddTransient<IExamineeExamQuestionRepository, ExamineeExamQuestionRepository>()
            .AddTransient<IImportController, ImportController>()
            .AddTransient<MainWindow>()
            .AddTransient<ImportWindow>()
            .AddTransient<ExamResultWindow>()
            ;

        AppService.BuildServiceProvider();

        MainWindow = AppService.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }
}