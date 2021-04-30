using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Transactions.Commands;
using WorkflowManagerMonolith.Application.Transactions.DTOs;

namespace WorkflowManagerMonolith.Application.Transactions
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> BrowseTransactionsAsync(GetTransactionsQuery query);
        Task<IEnumerable<TransactionDto>> GetInitialTransactionsAsync();
        Task<TransactionDto> GetTransactionByIdAsync(Guid id);
        Task CreateTransactionAsync(CreateTransactionCommand command);
        Task UpdateTransactionAsync(Guid Id, UpdateTransactionCommand command);
    }
}
