using Microsoft.EntityFrameworkCore;

namespace WorkflowManager.IdentityService.Infrastructure.Context
{
    public class IdentityDatabaseContext : DbContext
    {
        public IdentityDatabaseContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
