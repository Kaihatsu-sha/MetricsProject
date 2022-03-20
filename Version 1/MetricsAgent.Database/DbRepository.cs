using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetricsAgent.Entities;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Database
{
    public class DbRepository<TEntity> : IDbRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public DbRepository(AppDbContext context, ILogger<DbRepository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"DbRepository [ERROR] : {ex.Message} {ex.StackTrace}");
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _context.Set<TEntity>().Remove(entity));
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _context.Set<TEntity>().Update(entity));
            await _context.SaveChangesAsync();
        }
    }
}
