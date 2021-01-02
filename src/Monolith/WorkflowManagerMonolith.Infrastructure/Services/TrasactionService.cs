using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Transactions.DTOs;
using WorkflowManagerMonolith.Core.Domain;
using WorkflowManagerMonolith.Core.Repositories;
using WorkflowManagerMonolith.Application.Transactions;

namespace WorkflowManagerMonolith.Infrastructure.Services
{
    public class TrasactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IMapper mapper;

        public TrasactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            this.transactionRepository = transactionRepository;
            this.mapper = mapper;
        }


        public async Task<IEnumerable<TransactionDto>> BrowseTransactionsAsync(GetTransactionsQuery query)
        {
            var transactions = await transactionRepository.GetAllAsync();
            return mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task CreateTransactionAsync(CreateTransactionCommand command)
        {
            var application = new TransactionEntity(
                command.Id,
                command.Name,
                command.Description,
                command.IncomingStatusId,
                command.OutgoingStatusId);

            await transactionRepository.CreateAsync(application);
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await GetByIdAsync(id);
            return mapper.Map<TransactionDto>(transaction);
        }

        public async Task UpdateTransactionAsync(Guid Id, UpdateTransactionCommand command)
        {
            var transaction = await GetByIdAsync(Id);

            if (!string.IsNullOrWhiteSpace(command.Name))
            {
                transaction.SetName(command.Name);
            }

            if (!string.IsNullOrWhiteSpace(command.Description))
            {
                transaction.SetDescription(command.Description);
            }

            if (command.IncomingStatusId.HasValue)
            {
                transaction.SetIncomingStatus(command.IncomingStatusId.Value);
            }

            if (command.OutgoingStatusId.HasValue)
            {
                transaction.SetOutgoingStatus(command.OutgoingStatusId.Value);
            }
        }

        private async Task<TransactionEntity> GetByIdAsync(Guid Id)
        {
            return await transactionRepository.GetAsync(Id);
        }
    }
}
