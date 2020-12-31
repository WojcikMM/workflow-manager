using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Models;

namespace WorkflowManagerMonolith.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ISet<ApplicationModel> Applications { get; set; }
        public ISet<TransactionItemModel> TransactionItems { get; set; }
        public ISet<TransactionModel> Transactions { get; set; }
        public ISet<StatusModel> Statuses { get; set; }

        public void SaveChanges();
        public Task SaveChangesAsync();
    }
}
