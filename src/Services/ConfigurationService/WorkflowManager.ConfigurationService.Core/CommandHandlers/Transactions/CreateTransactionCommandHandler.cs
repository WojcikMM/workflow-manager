using MassTransit;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Commands.Transactions;
using WorkflowManager.Common.Messages.Events.Transactions.Rejected;
using WorkflowManager.ConfigurationService.Core.Domain;
using WorkflowManager.CQRS.Domain.Exceptions;
using WorkflowManager.CQRS.Storage;

namespace WorkflowManager.ConfigurationService.Core.CommandHandlers.Transactions
{
    public class CreateTransactionCommandHandler : BaseCommandHandler<CreateTransactionCommand, Transaction>
    {
        public IRepository<Status> _statusesRepository { get; }

        public CreateTransactionCommandHandler(IRepository<Transaction> transactionRepository, IRepository<Status> statusesRepository) : base(transactionRepository)
        {
            _statusesRepository = statusesRepository;
        }

        public override void HandleCommand(CreateTransactionCommand command, ConsumeContext<CreateTransactionCommand> context)
        {
            if (!_statusesRepository.Any(command.StatusId))
            {
                // Publish rejection event to inform about fail in command handler
                var message = $"Cannot create this transaction because given status with Id = {command.StatusId} not exists.";
                context.Publish(new TransactionNotCreatedEvent(command.AggregateId, message));
                throw new AggregateInternalLogicException(message);
            }

            if (!_statusesRepository.Any(command.OutgoingStatusId))
            {
                // Publish rejection event to inform about fail in command handler
                var message = $"Cannot create this transaction because given outgoingStatus with Id = {command.StatusId} not exists.";
                context.Publish(new TransactionNotCreatedEvent(command.AggregateId, message));
                throw new AggregateInternalLogicException(message);
            }
            aggregate = new Transaction(command.AggregateId, command.Name, command.Description, command.StatusId, command.OutgoingStatusId);
        }
    }
}
