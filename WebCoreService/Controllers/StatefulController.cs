using System;
using System.Fabric;
using System.Fabric.Query;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using StatefulCounterService.Services;
using WebCoreService.Config;

namespace WebCoreService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StatefulController : Controller
    {
        private readonly StatelessServiceContext _serviceContext;
        private readonly FabricClient _fabricClient;
        private readonly ConfigSettings _configSettings;

        public StatefulController(StatelessServiceContext serviceContext,
            FabricClient fabricClient,
            ConfigSettings configSettings)
        {
            _serviceContext = serviceContext;
            _fabricClient = fabricClient;
            _configSettings = configSettings;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            string serviceUri = $"{_serviceContext.CodePackageActivationContext.ApplicationName}/{_configSettings.StatefulServiceName}";

            ServicePartitionList partitions = await _fabricClient.QueryManager.GetPartitionListAsync(new Uri(serviceUri));
            var partition = new ServicePartitionKey(1);

            var t = await _fabricClient.ServiceManager.GetServiceDescriptionAsync(new Uri(serviceUri));

            IStatefulCounterService proxy = ServiceProxy.Create<IStatefulCounterService>(new Uri(serviceUri), partition);

            var result = await proxy.GetCountAsync();

            return Ok(new
            {
                result.counter,
                result.node
            });
        }
    }
}