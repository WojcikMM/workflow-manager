using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Transactions.DTOs;

namespace WorkflowManagerMonolith.Transaction.Transactions
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> BrowseTransactionsAsync(GetTransactionsQuery query);
        Task<TransactionDto> GetTransactionByIdAsync(Guid id);
        Task<TransactionDto> CreateTransactionAsync(CreateTransactionCommand command);
        Task<TransactionDto> UpdateTransactionAsync(UpdateTransactionCommand command);

    }
}
