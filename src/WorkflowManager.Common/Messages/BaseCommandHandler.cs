using System;
using System.Threading.Tasks;
using MassTransit;
using WorkflowManager.CQRS.Domain.Commands;
using WorkflowManager.CQRS.Domain.Domain;
using WorkflowManager.CQRS.Storage;

namespace WorkflowManager.Common.Messages
{
    public abstract class BaseCommandHandler<TCommand, TAggregate> : IConsumer<TCommand>
        where TCommand : class, ICommand
        where TAggregate : AggregateRoot, new()
    {
        protected readonly IRepository<TAggregate> _repository;
        protected TAggregate aggregate;

        public BaseCommandHandler(IRepository<TAggregate> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Consume(ConsumeContext<TCommand> context)
        {
            if (context.Message is null)
            {
                throw new ArgumentNullException(nameof(context.Message), "Passed command value is null.");
            }

            HandleCommand(context.Message, context);

            if (aggregate is null)
            {
                throw new ArgumentNullException(nameof(aggregate), "Cannot save null valued aggregate.");
            }

            await _repository.SaveAsync(aggregate, context.Message.Version, context.CorrelationId.GetValueOrDefault());

            await Task.CompletedTask;
        }

        public abstract void HandleCommand(TCommand command, ConsumeContext<TCommand> context);
    }
}
