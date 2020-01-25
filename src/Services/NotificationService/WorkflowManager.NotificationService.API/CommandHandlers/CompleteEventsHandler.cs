using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WorkflowManager.Common.Messages.Events.Processes;
using WorkflowManager.Common.Messages.Events.Saga;
using WorkflowManager.CQRS.Domain.EventHandlers;
using WorkflowManager.NotificationService.API.HubConfig;

namespace WorkflowManager.NotificationService.API.CommandHandlers
{
    public class CompleteEventsHandler :
        IEventHandler<BaseCompleteEvent>,
        IEventHandler<BaseRejectedEvent>

    {
        private readonly IHubContext<EventHub> _hubContext;

        public CompleteEventsHandler(IHubContext<EventHub> hubContext)
        {
            this._hubContext = hubContext;
        }

        public async Task HandleAsync(BaseCompleteEvent @event, Guid correlationId)
        {
            await _hubContext.Clients.All.SendAsync("operation_complete", @event);
        }

        public async Task HandleAsync(BaseRejectedEvent @event, Guid correlationId)
        {
            await _hubContext.Clients.All.SendAsync("operation_rejected", @event);
        }
    }
}
