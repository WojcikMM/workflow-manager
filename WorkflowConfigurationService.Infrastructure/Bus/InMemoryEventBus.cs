using System;
using CQRS.Template.Domain.Bus;
using CQRS.Template.Domain.EventHandlers;
using CQRS.Template.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace WorkflowConfigurationService.Infrastructure.Bus
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        public void Publish<TEvent>(TEvent @event) where TEvent : BaseEvent
        {
            var eventHandlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
            foreach (var eventHandler in eventHandlers)
            {
                eventHandler.Handle(@event);
            }
        }
    }
}
