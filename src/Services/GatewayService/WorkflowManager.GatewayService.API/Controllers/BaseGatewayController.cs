using System;
using RestEase;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Common.ApiResponses;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.Gateway.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseGatewayController : ControllerBase
    {
        private readonly IBusPublisher _busPublisher;

        protected BaseGatewayController(IBusPublisher busPublisher) =>
            _busPublisher = busPublisher;


        protected IActionResult ReturnResponse<T>(Response<T> response)
        {
            int statusCode = (int)response.ResponseMessage.StatusCode;
            if (response.ResponseMessage.IsSuccessStatusCode)
            {
                return StatusCode(statusCode, response.GetContent());
            }
            else
            {
                switch (response.ResponseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        var result = JsonConvert.DeserializeObject<NotFoundResponse>(response.StringContent);
                        return NotFound(result);
                    default:
                        var defaultResult = JsonConvert.DeserializeObject<Exception>(response.StringContent);

                        return StatusCode(statusCode,defaultResult);
                }
            }

        }

        protected async Task<IActionResult> SendAsync<T>(T command) where T : ICommand
        {
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