using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Controllers
{
    public class BaseWithPublisherController : BaseController
    {
        private readonly IBusPublisher _busPublisher;

        protected BaseWithPublisherController(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        protected async Task<IActionResult> SendAsync<T>(T command) where T : ICommand
        {
            //var context = GetContext<T>(resourceId, resource);
            Guid correlationId = Guid.NewGuid();
            await _busPublisher.SendAsync(command, correlationId);

            return Accepted(new
            {
                AggregateId = command.Id,
                CorrelationId = correlationId
            });
        }
    }
}