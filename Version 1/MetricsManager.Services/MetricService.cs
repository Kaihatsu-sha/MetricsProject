using MetricsManager.Database;
using MetricsManager.Entities;
using MetricsManager.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Services
{
    public class MetricService<TRequest, TResponse> : IMetricService<TResponse> where TResponse : BaseEntityDto
    {
        private readonly IHttpClientService<TResponse> _httpClient;
        private readonly IDbRepository<AgentInfo> _repository;

        public MetricService(IDbRepository<AgentInfo> repository, IHttpClientFactory factory)
        {
            _repository = repository;
            _httpClient = new HttpClientService<TResponse>(typeof(TRequest).ToString(), factory);
        }


        public List<TResponse> GetFromAgent(int agentId, TimeSpan fromTime, TimeSpan toTime)
        {
            AgentInfo agent = _repository.GetAll().Where(entity => entity.Id == agentId).FirstOrDefault();

            if (agent == null)
            {
                throw new ArgumentNullException("Database not have agent from id");
            }
            return GetResult(agent, fromTime, toTime);
        }

        public List<TResponse> GetFromCluster(TimeSpan fromTime, TimeSpan toTime)
        {
            List<AgentInfo> agents = _repository.GetAll().ToList();

            if (agents == null || agents.Count == 0)
            {
                throw new ArgumentNullException("Database not have agents");
            }

            List<TResponse> result = new List<TResponse>();
            foreach (AgentInfo agent in agents)
            {
                result.AddRange(GetResult(agent, fromTime, toTime));
            }

            return result;
        }

        private List<TResponse> GetResult(AgentInfo agent, TimeSpan fromTime, TimeSpan toTime)
        {
            //TODO Маппинг с добавлением AgentId
            if (typeof(TRequest) == typeof(CpuMetric))
            {
                return _httpClient.GetResponse(new Uri($"{agent.Address}api/metrics/cpu/from/{fromTime}/to/{toTime}")).Result;
            }
            else if (typeof(TRequest) == typeof(DotNetMetric))
            {
                return _httpClient.GetResponse(new Uri($"{agent.Address}api/metrics/dotnet/errors-count/from/{fromTime}/to/{toTime}")).Result;
            }
            else if (typeof(TRequest) == typeof(NetworkMetric))
            {
                return _httpClient.GetResponse(new Uri($"{agent.Address}api/metrics/network/from/{fromTime}/to/{toTime}")).Result;
            }
            else if (typeof(TRequest) == typeof(RamMetric))
            {
                return _httpClient.GetResponse(new Uri($"{agent.Address}api/metrics/ram/available/from/{fromTime}/to/{toTime}")).Result;
            }
            else if (typeof(TRequest) == typeof(HddMetric))
            {
                return _httpClient.GetResponse(new Uri($"{agent.Address}api/metrics/hdd/left/from/{fromTime}/to/{toTime}")).Result;
            }

            throw new ArgumentException($"Type is not valid {typeof(TRequest)}");
        }

    }
}
