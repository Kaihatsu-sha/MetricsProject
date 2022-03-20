using MetricsManager.Entities;
using MetricsManager.Services.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MetricsManager.Services
{
    public interface IHttpClientService<TEntity> where TEntity : BaseEntityDto
    {
        Task<List<TEntity>> GetResponse(Uri url);
    }
}
