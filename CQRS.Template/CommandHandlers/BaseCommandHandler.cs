using System;
using System.Threading.Tasks;
using CQRS.Template.Domain.Domain;
using CQRS.Template.Domain.Storage;
using CQRS.Template.Domain.Commands;

namespace CQRS.Template.Domain.CommandHandlers
{
    public abstract class BaseCommandHandler<TCommand, TAggregate>: ICommandHandler<TCommand> where TCommand : BaseCommand where TAggregate : AggregateRoot, new()
    {
        protected readonly IRepository<TAggregate> _repository;
        protected TAggregate aggregate;

        public BaseCommandHandler(IRepository<TAggregate> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

       public async Task HandleAsync(TCommand command,Guid correlationId)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command), "Passed command value is null.");
            }

            HandleCommand(command);

            if(aggregate is null)
            {
                throw new ArgumentNullException(nameof(aggregate), "Cannot save null valued aggregate.");
            }

           await _repository.SaveAsync(aggregate, command.Version, correlationId);

            await Task.CompletedTask;
        }

        public abstract void HandleCommand(TCommand command); 
    }
}
