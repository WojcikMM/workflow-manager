using WorkflowManager.CQRS.Domain.Commands;
using WorkflowManager.CQRS.Domain.Events;
using System;
using System.Threading.Tasks;

namespace WorkflowManager.Common.RabbitMq
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, Guid correlationId) where TCommand : ICommand;

        Task PublishAsync<TEvent>(TEvent @event, Guid correlationId) where TEvent : IEvent;
    }
}
