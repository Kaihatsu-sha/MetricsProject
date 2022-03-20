using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetricsManager.Entities;
using MetricsManager.Database;
using AutoMapper;
using System.Net.Http;

namespace MetricsManager.Services.Repository
{
    public class RepositoryService<TEntity, TDtoEntity> : IRepositoryService<TDtoEntity>
        where TEntity : AgentInfo//TODO FIX
        where TDtoEntity : BaseEntityDto
    {
        private readonly IDbRepository<AgentInfo> _repository;
        private readonly IMapper _mapper;

        public RepositoryService(IDbRepository<AgentInfo> repository)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<TDtoEntity, TEntity>());
            _mapper = config.CreateMapper();
            _repository = repository;
        }

        public async Task AddAsync(TDtoEntity dtoEntity)
        {
            var en = _mapper.Map<TEntity>(dtoEntity);
            await _repository.AddAsync(en);
        }

        public async Task DeleteAsync(TDtoEntity dtoEntity)
        {
            await _repository.DeleteAsync(_mapper.Map<TEntity>(dtoEntity));
        }

        public async Task DisableAsync(int agentId)
        {
            AgentInfo agent = _repository.GetAll().Where(item => item.Id == agentId).FirstOrDefault();
            agent.CanAvailable = false;
            await _repository.UpdateAsync(agent);
        }

        public async Task EnableAsync(int agentId)
        {
            AgentInfo agent = _repository.GetAll().Where(item => item.Id == agentId).FirstOrDefault();
            agent.CanAvailable = true;
            await _repository.UpdateAsync(agent);
        }

        public IQueryable<TDtoEntity> GetAll()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TDtoEntity>());
            IMapper mapper = config.CreateMapper();

            IList<AgentInfo> metrics = _repository.GetAll().ToList();
            IList<TDtoEntity> dtoMetrics = new List<TDtoEntity>();

            foreach (var metric in metrics)
            {
                dtoMetrics.Add(mapper.Map<TDtoEntity>(metric));
            }

            return dtoMetrics.AsQueryable();
        }

        public async Task UpdateAsync(TDtoEntity dtoEntity)
        {
            await _repository.UpdateAsync(_mapper.Map<TEntity>(dtoEntity));
        }
    }
}