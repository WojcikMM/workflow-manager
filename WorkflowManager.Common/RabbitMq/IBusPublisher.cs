using System;
using System.Threading.Tasks;
using CQRS.Template.Domain.Commands;
using CQRS.Template.Domain.Events;

namespace WorkflowManager.Common.RabbitMq
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, Guid correlationId) where TCommand : ICommand;

        Task PublishAsync<TEvent>(TEvent @event, Guid correlationId) where TEvent : IEvent;
    }
}
