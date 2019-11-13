using System;
using WorkflowConfigurationService.Domain.Bus;
using WorkflowConfigurationService.Domain.Events;

namespace WorkflowConfigurationService.Infrastructure.Bus
{
    public class InMemoryEventBus : IEventBus
    {

        private readonly IServiceProvider _serviceProvider;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        public void Publish<T>(T @event) where T : BaseEvent
        {
            //var handlers = _serviceProvider.GetService<IEventHandler<T>>();
            //foreach (var eventHandler in handlers)
            //{
            //    eventHandler.Handle(@event);
            //}
        }
    }
}
