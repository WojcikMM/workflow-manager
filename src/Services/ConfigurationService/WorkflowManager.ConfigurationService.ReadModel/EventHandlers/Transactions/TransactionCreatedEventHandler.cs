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
    public class TransactionCreatedEventHandler : BaseEventHandler<TransactionCreatedEvent>
    {
        private IReadModelRepository<TransactionModel> _repository;

        public TransactionCreatedEventHandler([NotNull] IReadModelRepository<TransactionModel> repository) => _repository = repository;

        public override async Task Consume(ConsumeContext<TransactionCreatedEvent> context)
        {
            var @event = context.Message;
            TransactionModel transaction = new TransactionModel()
            {
                Id = @event.AggregateId,
                Name = @event.Name,
                Description = @event.Description,
                StatusId = @event.StatusId,
                OutgoingStatusId = @event.OutgoingStatusId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Version = @event.Version
            };

            await _repository.AddAsync(transaction);
        }
    }
}
