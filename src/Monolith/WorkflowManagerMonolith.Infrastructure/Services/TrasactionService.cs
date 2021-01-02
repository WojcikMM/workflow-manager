using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Transactions.DTOs;
using WorkflowManagerMonolith.Transaction.Transactions;

namespace WorkflowManagerMonolith.Infrastructure.Services
{
    public class TrasactionService : ITransactionService
    {
        public Task<IEnumerable<TransactionDto>> BrowseTransactionsAsync(GetTransactionsQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionDto> CreateTransactionAsync(CreateTransactionCommand command)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionDto> GetTransactionByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionDto> UpdateTransactionAsync(UpdateTransactionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
