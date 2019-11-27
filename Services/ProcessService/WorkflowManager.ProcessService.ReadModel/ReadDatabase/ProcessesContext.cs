using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace WorkflowManager.ProcessService.ReadModel.ReadDatabase
{
    public class ProcessesContext : DbContext
    {
        public ProcessesContext() : base() { }
        public ProcessesContext(DbContextOptions<ProcessesContext> options) : base(options) { }

        public DbSet<ProcessModel> Processes { get; set; }
    }
}
