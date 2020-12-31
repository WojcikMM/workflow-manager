using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Core.Domain;
using WorkflowManagerMonolith.Core.Repositories;
using WorkflowManagerMonolith.Application.Models;
using WorkflowManagerMonolith.Application.UnitOfWork;

namespace WorkflowManagerMonolith.Application.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TransactionRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateAsync(TransactionEntity entity)
        {
            var transactionModel = mapper.Map<TransactionModel>(entity);
            unitOfWork.Transactions.Add(transactionModel);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<TransactionEntity> GetAsync(Guid id)
        {
            var transactionModel = GetModelById(id);
            var transaction = mapper.Map<TransactionEntity>(transactionModel);
            return await Task.FromResult(transaction);
        }

        public async Task UpdateAsync(TransactionEntity entity)
        {
            var transaction = GetModelById(entity.Id);
            transaction.Name = entity.Name;
            transaction.Description = entity.Description;
            transaction.IncomingStatusId = entity.IncomingStatusId;
            transaction.OutgoingStatusId = entity.OutgoingStatusId;

            await unitOfWork.SaveChangesAsync();
        }

        private TransactionModel GetModelById(Guid id)
        {
            return unitOfWork.Transactions.FirstOrDefault(transaction => transaction.Id == id);
        }
    }
}
