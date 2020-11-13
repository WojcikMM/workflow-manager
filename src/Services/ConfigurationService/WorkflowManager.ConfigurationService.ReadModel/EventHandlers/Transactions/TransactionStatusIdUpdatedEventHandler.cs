using MassTransit;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Events.Transactions;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;
using WorkflowManager.CQRS.ReadModel;

namespace WorkflowManager.ConfigurationService.ReadModel.EventHandlers.Transactions
{
    public class TransactionStatusIdUpdatedEventHandler : BaseEventHandler<TransactionStatusIdUpdatedEvent>
    {
        private IReadModelRepository<TransactionModel> _repository;

        public TransactionStatusIdUpdatedEventHandler([NotNull] IReadModelRepository<TransactionModel> repository) => _repository = repository;
        public override async Task Consume(ConsumeContext<TransactionStatusIdUpdatedEvent> context)
        {
            var transaction = await _repository.GetByIdAsync(context.Message.AggregateId);
            transaction.StatusId = context.Message.StatusId;
            transaction.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(transaction);
        }
    }
}
