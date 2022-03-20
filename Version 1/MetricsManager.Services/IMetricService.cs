using MetricsManager.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Services
{
    public interface IMetricService<TResponce> where TResponce : BaseEntityDto
    {
        List<TResponce> GetFromAgent(int agentId, TimeSpan fromTime, TimeSpan toTime);

        List<TResponce> GetFromCluster(TimeSpan fromTime, TimeSpan toTime);
    }
}
