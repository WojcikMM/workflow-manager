using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Models;
using WorkflowManagerMonolith.Application.UnitOfWork;

namespace WorkflowManagerMonolith.Infrastructure.EntityFramework
{
    public class WorkflowManagerDbContext : IUnitOfWork
    {
        public ISet<ApplicationModel> Applications { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public ISet<TransactionItemModel> TransactionItems { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public ISet<TransactionModel> Transactions { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public ISet<StatusModel> Statuses { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
