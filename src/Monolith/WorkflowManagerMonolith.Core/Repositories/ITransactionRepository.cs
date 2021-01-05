using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Core.Abstractions;
using WorkflowManagerMonolith.Core.Domain;

namespace WorkflowManagerMonolith.Core.Repositories
{
    public interface ITransactionRepository : IRepository<TransactionEntity>
    {
        Task<IEnumerable<TransactionEntity>> GetByIncomingStatusAsync(Guid statusId);
        Task<IEnumerable<TransactionEntity>> GetStartingTransactions();
    }
}
