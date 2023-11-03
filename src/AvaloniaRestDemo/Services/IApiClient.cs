using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaRestDemo.Services;

public interface IApiClient
{
    Task<RestResponse<T>> ExecuteGetAsync<T>(string endpoint, CancellationToken cancellationToken = default);
}

