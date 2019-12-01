using Microsoft.EntityFrameworkCore;

namespace WorkflowManager.ProcessService.ReadModel.ReadDatabase
{
    public class ProcessesContext : DbContext
    {
        public ProcessesContext(DbContextOptions<ProcessesContext> options) : base(options) { }

        public DbSet<ProcessModel> Processes { get; set; }
    }
}
