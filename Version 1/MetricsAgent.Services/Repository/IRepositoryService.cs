using MetricsAgent.Services.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Services.Repository
{
    public interface IRepositoryService<TEntity> where TEntity : BaseEntityDto
    {
        IQueryable<TEntity> GetAll();
        Task AddAsync(TEntity dtoEntity);
        Task UpdateAsync(TEntity dtoEntity);
        Task DeleteAsync(TEntity dtoEntity);
    }
}
