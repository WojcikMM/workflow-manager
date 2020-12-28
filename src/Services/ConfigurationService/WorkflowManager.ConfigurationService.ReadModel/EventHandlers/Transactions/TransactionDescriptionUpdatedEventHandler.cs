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
    public class TransactionDescriptionUpdatedEventHandler : BaseEventHandler<TransactionDescriptionUpdatedEvent>
    {
        private IReadModelRepository<TransactionModel> _repository;

        public TransactionDescriptionUpdatedEventHandler([NotNull] IReadModelRepository<TransactionModel> repository) => _repository = repository;
        public override async Task Consume(ConsumeContext<TransactionDescriptionUpdatedEvent> context)
        {
            var transaction = await _repository.GetByIdAsync(context.Message.AggregateId);
            transaction.Description = context.Message.Description;
            transaction.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(transaction);
        }
    }
}
