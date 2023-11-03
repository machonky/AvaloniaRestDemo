using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaRestDemo.Services;
using AvaloniaRestDemo.ViewModels;
using AvaloniaRestDemo.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AvaloniaRestDemo;

public partial class App : Application
{
    public new static App? Current => Application.Current as App;

    private ServiceCollection ServiceCollection { get; set; } = new ServiceCollection();
    public IServiceProvider? Services { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ConfigureServices();

        // Build service provider
        Services = ServiceCollection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = BuildMainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = BuildMainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private MainViewModel BuildMainViewModel()
    {
        return Services!.GetRequiredService<MainViewModel>();
    }

    private void ConfigureServices()
    {
        ServiceCollection
            .AddApplicationDispatcher()
            .AddConfiguration()
            .AddApiClient()
            .AddDataServices()
            .AddViewModelServices();
    }
}