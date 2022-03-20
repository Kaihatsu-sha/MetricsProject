using MetricsManager.Services;
using MetricsManager.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.API.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : BaseController<HddMetricsController, HddMetricDto>
    {
        public HddMetricsController(ILogger<HddMetricsController> logger, IMetricService<HddMetricDto> service)
            : base(logger, service)
        {
        }
    }
}
