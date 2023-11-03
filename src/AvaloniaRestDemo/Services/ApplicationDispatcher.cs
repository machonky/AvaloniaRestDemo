using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AvaloniaRestDemo.Services;

public class ApplicationDispatcher : IApplicationDispatcher
{
    private static Dispatcher Dispatcher => Dispatcher.UIThread;

    public void Dispatch(Action action) => Dispatcher.Post(action);

    public Task DispatchAsync(Func<Task> task) => Dispatcher.InvokeAsync(task);
}

public static class ApplicationDispatcherExtensions
{
    public static IServiceCollection AddApplicationDispatcher(this IServiceCollection services)
    {
        return services
            .AddSingleton<IApplicationDispatcher, ApplicationDispatcher>();
    }
}
