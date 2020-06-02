using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using WorkflowManager.Common.Messages;
using WorkflowManager.Common.Messages.Events.Saga;
using WorkflowManager.NotificationsService.API.HubConfig;

namespace WorkflowManager.NotificationsService.API.CommandHandlers
{
    public class RejectEventHandler : BaseEventHandler<BaseRejectedEvent>
    {
        private readonly IHubContext<EventHub> _hubContext;

        public RejectEventHandler(IHubContext<EventHub> hubContext)
        {
            this._hubContext = hubContext;
        }
        public override async Task Consume(ConsumeContext<BaseRejectedEvent> context)
        {
            await _hubContext.Clients.All.SendAsync("operation_rejected", context.Message);
        }
    }
}
