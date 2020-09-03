﻿using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.CQRS.Storage;
using WorkflowManager.StatusesService.Core.Domain;

namespace WorkflowManager.StatusesService.Core.CommandHandlers
{
    public class RemoveStatusCommandHandler : BaseCommandHandler<RemoveStatusCommand, Status>
    {
        public RemoveStatusCommandHandler(IRepository<Status> repository) : base(repository)
        {
        }

        public override void HandleCommand(RemoveStatusCommand command)
        {
            aggregate = _repository.GetById(command.AggregateId);
            aggregate.Remove();
        }
    }
}
