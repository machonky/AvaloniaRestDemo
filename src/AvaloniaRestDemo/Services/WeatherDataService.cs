using AvaloniaRestDemo.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaRestDemo.Services;

public class WeatherDataService : IEntitiesDataService<WeatherForecast>
{
    private readonly IApiClient client;

    public WeatherDataService(IApiClient client)
    {
        this.client = client;
    }

    public async Task<IEnumerable<WeatherForecast>> FetchEntitiesAsync()
    {
        var resource = "WeatherForecast";
        var response = await client.ExecuteGetAsync<IEnumerable<WeatherForecast>>(resource);
        if (response.IsSuccessful)
        {
            return response.Data;
        }
        else 
        { 
            // Inspect what went wrong
        }

        return Array.Empty<WeatherForecast>();
    }
}

public static class DataServiceExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        return services.AddSingleton<IEntitiesDataService<WeatherForecast>, WeatherDataService>();
    }
}
