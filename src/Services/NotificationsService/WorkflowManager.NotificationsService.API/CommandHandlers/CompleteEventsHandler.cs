using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Events.Saga;
using WorkflowManager.NotificationsService.API.HubConfig;

namespace WorkflowManager.NotificationsService.API.CommandHandlers
{
    public class CompleteEventsHandler : BaseEventHandler<BaseCompleteEvent>

    {
        private readonly IHubContext<EventHub> _hubContext;

        public CompleteEventsHandler(IHubContext<EventHub> hubContext) => _hubContext = hubContext;

        public override async Task Consume(ConsumeContext<BaseCompleteEvent> context)
        {
            await _hubContext.Clients.All.SendAsync("operation_complete", context.Message);
        }
    }
}
