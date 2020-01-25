using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Gateway.API.DTOs;
using WorkflowManager.Common.ApiResponses;
using WorkflowManager.Gateway.API.Services;

namespace WorkflowManager.Gateway.API.Controllers
{
    public class OperationsController : BaseGatewayController
    {
        private readonly IOperationsService _operationsService;
        public OperationsController(IOperationsService operationsService, IBusPublisher busPublisher) : base(busPublisher) => _operationsService = operationsService;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OperationDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id) =>
            ReturnResponse(await _operationsService.GetAsync(id));
    }
}