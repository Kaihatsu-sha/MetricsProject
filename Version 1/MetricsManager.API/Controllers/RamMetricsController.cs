using MetricsManager.Services;
using MetricsManager.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.API.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : BaseController<RamMetricsController, RamMetricDto>
    {
        public RamMetricsController(ILogger<RamMetricsController> logger, IMetricService<RamMetricDto> service)
            : base(logger, service)
        {
        }
    }
}
