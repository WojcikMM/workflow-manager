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
    public class TransactionNameUpdatedEventHandler : BaseEventHandler<TransactionNameUpdatedEvent>
    {
        private IReadModelRepository<TransactionModel> _repository;

        public TransactionNameUpdatedEventHandler([NotNull] IReadModelRepository<TransactionModel> repository) => _repository = repository;
        public override async Task Consume(ConsumeContext<TransactionNameUpdatedEvent> context)
        {
            var transaction = await _repository.GetByIdAsync(context.Message.AggregateId);
            transaction.Name = context.Message.Name;
            transaction.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(transaction);
        }
    }
}
