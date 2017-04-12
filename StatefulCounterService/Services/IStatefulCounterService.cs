using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace StatefulCounterService.Services
{
    public interface IStatefulCounterService : IService
    {
        Task<(long counter, string node)> GetCountAsync();
    }
}