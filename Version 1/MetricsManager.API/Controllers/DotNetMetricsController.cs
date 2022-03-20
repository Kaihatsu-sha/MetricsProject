using MetricsManager.Services;
using MetricsManager.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.API.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : BaseController<DotNetMetricsController, DotNetMetricDto>
    {
        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IMetricService<DotNetMetricDto> service)
            : base(logger, service)
        {
        }

    }
}
