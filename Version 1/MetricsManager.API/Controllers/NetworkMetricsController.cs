using MetricsManager.Services;
using MetricsManager.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.API.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : BaseController<NetworkMetricsController, NetworkMetricDto>
    {
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IMetricService<NetworkMetricDto> service)
            : base(logger, service)
        {
        }
    }
}
