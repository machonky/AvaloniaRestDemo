using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaloniaRestDemo.Services;

public interface IEntitiesDataService<T>
{
    Task<IEnumerable<T>> FetchEntitiesAsync();
}
