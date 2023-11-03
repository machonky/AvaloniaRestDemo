using Avalonia.Animation;
using AvaloniaRestDemo.Contracts;
using AvaloniaRestDemo.Services;
using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AvaloniaRestDemo.ViewModels;

public class MainViewModel : ViewModelBase
{
    private IEntitiesDataService<WeatherForecast> dataService;
    private IApplicationDispatcher dispatcher;

    public string Greeting => "Welcome to Avalonia!";

    public MainViewModel(IEntitiesDataService<WeatherForecast> dataService, IApplicationDispatcher dispatcher)
    {
        this.dataService = dataService;
        this.dispatcher = dispatcher;

        Entities = new ObservableCollection<WeatherForecast>();

        LoadEntities();
    }

    public ObservableCollection<WeatherForecast> Entities { get; }

    private async Task LoadEntities()
    {
        var result = await dataService.FetchEntitiesAsync();
        dispatcher.Dispatch(() =>
        {
            // Sending to the UI must be done in the UI Thread not the worker thread
            Entities.AddRange(result);
        });
    }
}

public static class ViewModelExtensions
{
    public static IServiceCollection AddViewModelServices(this IServiceCollection services)
    {
        services.AddTransient<MainViewModel>();

        return services;
    }
}
