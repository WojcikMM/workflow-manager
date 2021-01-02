using Microsoft.EntityFrameworkCore;

namespace WorkflowManagerMonolith.Infrastructure.EntityFramework
{
    public class WorkflowManagerDbContext : DbContext
    {
        public DbSet<ApplicationModel> Applications { get; set; }
        public DbSet<TransactionItemModel> TransactionItems { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }
    }
}
