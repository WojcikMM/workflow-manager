using Microsoft.EntityFrameworkCore;

namespace WorkflowManager.ProcessService.ReadModel.ReadDatabase
{
    public class ProcessesContext : DbContext
    {
        public ProcessesContext(DbContextOptions<ProcessesContext> options) : base(options) 
        {
            this.Database.EnsureCreated();
        }

        public DbSet<ProcessModel> Processes { get; set; }
    }
}
