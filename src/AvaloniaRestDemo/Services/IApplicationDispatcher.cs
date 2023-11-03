using System;
using System.Threading.Tasks;

namespace AvaloniaRestDemo.Services;

public interface IApplicationDispatcher
{
    void Dispatch(Action action);

    Task DispatchAsync(Func<Task> task);
}
