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
            await _busPublisher.Publish(command, typeof(T));

            return Accepted(new
            {
                command.AggregateId,
                command.CorrelationId
            });

        }
    }
}