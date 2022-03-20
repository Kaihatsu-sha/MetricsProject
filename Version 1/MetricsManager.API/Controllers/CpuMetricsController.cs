using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MetricsManager.Services.Repository;
using MetricsManager.Services;

namespace MetricsManager.API.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : BaseController<CpuMetricsController, CpuMetricDto>
    {

        public CpuMetricsController(ILogger<CpuMetricsController> logger, IMetricService<CpuMetricDto> service)
            : base(logger, service)
        {
        }
    }
}
