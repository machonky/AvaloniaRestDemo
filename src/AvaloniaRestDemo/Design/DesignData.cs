using AvaloniaRestDemo.Contracts;
using AvaloniaRestDemo.Services;
using AvaloniaRestDemo.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaRestDemo.Design;

public class DesignData
{
    private static readonly ServiceCollection services;
    private static readonly IServiceProvider serviceProvider;
    private static readonly IConfiguration config;
    private static readonly IApplicationDispatcher dispatcher;


    static DesignData()
    {
        services = new ServiceCollection();
        dispatcher = new ApplicationDispatcher();
        config = CreateDesignTimeConfiguration();

        services.AddSingleton<IApplicationDispatcher, ApplicationDispatcher>();
        services.AddSingleton<IEntitiesDataService<WeatherForecast>, DesignDataService>();
        services.AddTransient<MainViewModel>();

        serviceProvider = services.BuildServiceProvider();

        MainViewModel = serviceProvider.GetRequiredService<MainViewModel>();
    }

    private static IConfiguration CreateDesignTimeConfiguration()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"WelcomeMessage", "Design Hello World"},
            {"WebApiApplicationUrl", "https://localhost:7260"}
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();
    }

    public static MainViewModel MainViewModel { get; }
}

public class DesignDataService : IEntitiesDataService<WeatherForecast>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing (Design)", "Bracing (Design)", "Chilly (Design)", "Cool (Design)", "Mild (Design)", "Warm (Design)", "Balmy (Design)", "Hot (Design)", "Sweltering (Design)", "Scorching (Design)"
    };

    public Task<IEnumerable<WeatherForecast>> FetchEntitiesAsync()
    {
        return Task.FromResult((IEnumerable<WeatherForecast>)
            Enumerable.Range(1, 5)
            .Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray());
    }
}
