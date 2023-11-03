using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaRestDemo.Services;

public class ApiClient : IApiClient, IDisposable
{
    public ApiClient(IConfiguration configuration)
    {
        var applicationUrl = configuration["WebApiApplicationUrl"];
        this.client = new RestClient(applicationUrl);
    }

    public Task<RestResponse<T>> ExecuteGetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        return client!.ExecuteGetAsync<T>(new RestRequest(endpoint), cancellationToken);
    }

    private bool disposedValue;
    private RestClient? client;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                client?.Dispose();
            }

            client = null;
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

public static class ApiClientExtensions
{
    public static IServiceCollection AddApiClient(this IServiceCollection services)
    {
        services.AddSingleton<IApiClient, ApiClient>();
        return services;
    }
}