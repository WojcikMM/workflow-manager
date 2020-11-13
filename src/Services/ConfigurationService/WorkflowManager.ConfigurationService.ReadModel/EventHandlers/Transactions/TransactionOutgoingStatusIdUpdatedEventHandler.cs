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
    public class TransactionOutgoingStatusIdUpdatedEventHandler : BaseEventHandler<TransactionOutgoingStatusIdUpdatedEvent>
    {
        private IReadModelRepository<TransactionModel> _repository;

        public TransactionOutgoingStatusIdUpdatedEventHandler([NotNull] IReadModelRepository<TransactionModel> repository) => _repository = repository;
        public override async Task Consume(ConsumeContext<TransactionOutgoingStatusIdUpdatedEvent> context)
        {
            var transaction = await _repository.GetByIdAsync(context.Message.AggregateId);
            transaction.OutgoingStatusId = context.Message.OutgoingStatusId;
            transaction.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(transaction);
        }
    }
}
