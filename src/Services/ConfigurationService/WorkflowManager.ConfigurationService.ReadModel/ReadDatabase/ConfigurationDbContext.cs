using Microsoft.EntityFrameworkCore;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;

namespace WorkflowManager.ConfigurationService.ReadModel.ReadDatabase
{
    public class ConfigurationDbContext : DbContext
    {
        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<ProcessModel> Processes { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
