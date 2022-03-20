using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.Database;
using MetricsAgent.Entities;

namespace MetricsAgent.Services.Repository
{
    public class RepositoryService<TEntity, TDtoEntity> : IRepositoryService<TDtoEntity>
        where TEntity : BaseEntity
        where TDtoEntity : BaseEntityDto
    {
        private readonly IDbRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public RepositoryService(IDbRepository<TEntity> repository)
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

        public IQueryable<TDtoEntity> GetAll()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TDtoEntity>());
            IMapper mapper = config.CreateMapper();

            IList<TEntity> metrics = _repository.GetAll().ToList();
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
