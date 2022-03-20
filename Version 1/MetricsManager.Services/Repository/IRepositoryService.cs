using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsManager.Entities;

namespace MetricsManager.Services.Repository
{
    public interface IRepositoryService<TEntity> where TEntity : BaseEntityDto
    {
        IQueryable<TEntity> GetAll();
        Task AddAsync(TEntity dtoEntity);
        Task UpdateAsync(TEntity dtoEntity);
        Task DeleteAsync(TEntity dtoEntity);
        Task DisableAsync(int agentId);
        Task EnableAsync(int agentId);
    }
}
