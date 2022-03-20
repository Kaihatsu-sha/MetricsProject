using System;
using Microsoft.EntityFrameworkCore;
using MetricsManager.Entities;

namespace MetricsManager.Database
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<AgentInfo> AgentsInfo { get; set; }
        //public DbSet<CpuMetric> CpuMetrics { get; set; }
        //public DbSet<DotNetMetric> DotNetMetrics { get; set; }
        //public DbSet<HddMetric> HddMetrics { get; set; }
        //public DbSet<NetworkMetric> NetworkMetrics { get; set; }
        //public DbSet<RamMetric> RamMetrics { get; set; }

        public AppDbContext()
        { }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseNpgsql("default");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
