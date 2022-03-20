using MetricsAgent.Services.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.API.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : BaseController<NetworkMetricsController, NetworkMetricDto>
    {

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IRepositoryService<NetworkMetricDto> service)
        : base(logger, service)
        {
        }
    }
}
