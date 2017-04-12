using System;
using System.Fabric;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using StatelessCounterService.Services;
using WebCoreService.Config;

namespace WebCoreService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StatelessController : Controller
    {
        private readonly StatelessServiceContext _serviceContext;
        private readonly ConfigSettings _configSettings;

        public StatelessController(StatelessServiceContext serviceContext, ConfigSettings configSettings)
        {
            _serviceContext = serviceContext;
            _configSettings = configSettings;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            string serviceUri = $"{_serviceContext.CodePackageActivationContext.ApplicationName}/{_configSettings.StatelessServiceName}";

            IStatelessCounterService proxy = ServiceProxy.Create<IStatelessCounterService>(new Uri(serviceUri));

            var result = await proxy.GetCountAsync();

            return Ok(new
            {
                result.counter,
                result.node
            });
        }
    }
}