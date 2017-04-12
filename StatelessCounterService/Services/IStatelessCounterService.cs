using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace StatelessCounterService.Services
{
    public interface IStatelessCounterService : IService
    {
        //Task<Tuple<long, string>> GetCountAsync();
        Task<(long counter, string node)> GetCountAsync();
    }
}
