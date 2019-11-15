using System;
using System.Threading.Tasks;
using CQRS.Template.ReadModel;
using CQRS.Template.Domain.Events;
using CQRS.Template.Domain.EventHandlers;

namespace WorkflowConfigurationService.Core.Processes.EventHandlers
{
    public abstract class BaseEventHandler<TEvent, TReadModel> : IEventHandler<TEvent> where TEvent : BaseEvent where TReadModel : IReadModel, new()
    {
        protected readonly IReadModelRepository<TReadModel> _readModelRepository;

        public BaseEventHandler(IReadModelRepository<TReadModel> readModelRepository)
        {
            _readModelRepository = readModelRepository ?? throw new ArgumentNullException(nameof(readModelRepository));
        }


        public abstract Task HandleAsync(TEvent handle);
    }
}
