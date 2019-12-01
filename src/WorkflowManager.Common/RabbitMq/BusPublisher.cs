using WorkflowManager.CQRS.Domain.Commands;
using WorkflowManager.CQRS.Domain.Events;
using RawRabbit.vNext.Disposable;
using System;
using System.Threading.Tasks;

namespace WorkflowManager.Common.RabbitMq
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient _busClient;

        public BusPublisher(IBusClient busClient)
        {
            _busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
        }

        public async Task PublishAsync<TEvent>(TEvent @event, Guid correlationId) where TEvent : IEvent =>
            await _busClient.PublishAsync(@event, correlationId);

        public async Task SendAsync<TCommand>(TCommand command, Guid correlationId) where TCommand : ICommand =>
            await _busClient.PublishAsync(command, correlationId);
    }
}
