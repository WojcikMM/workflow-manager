using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Common.Controllers
{
    public class BaseWithPublisherController : BaseController
    {
        private readonly IPublishEndpoint _busPublisher;

        protected BaseWithPublisherController(IPublishEndpoint busPublisher)
        {
            _busPublisher = busPublisher;
        }

        protected async Task<IActionResult> SendAsync<T>(T command) where T : ICommand
        {
            var correlationId = Guid.Empty;
            //var context = GetContext<T>(resourceId, resource);
            await _busPublisher.Publish(command,publishCallback => {
                correlationId = publishCallback.CorrelationId.GetValueOrDefault();
            });

            return Accepted(new
            {
                AggregateId = command.Id,
                CorrelationId = correlationId
            });

        }
    }
}