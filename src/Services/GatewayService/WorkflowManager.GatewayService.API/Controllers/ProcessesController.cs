using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Gateway.API.DTOs;
using WorkflowManager.Gateway.API.Commands;
using WorkflowManager.Gateway.API.Services;
using WorkflowManager.Common.ApiResponses;
using WorkflowManager.Common.Messages.Commands.Processes;
using Microsoft.AspNetCore.Authorization;

namespace WorkflowManager.Gateway.API.Controllers
{
    public class ProcessesController : BaseGatewayController
    {
        private readonly IProcessesService _processesService;

        public ProcessesController(IProcessesService processesService, IBusPublisher busPublisher) : base(busPublisher) => _processesService = processesService;

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<ProcessDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] string name = null)
            => ReturnResponse(await _processesService.BrowseAsync(name));


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProcessDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
         => ReturnResponse(await _processesService.GetAsync(id));


        [HttpPost]
        [ProducesResponseType(typeof(AcceptedResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] CreateProcessCommandDTO dto) =>
            await SendAsync(new CreateProcessCommand(Guid.NewGuid(), dto.Name));


        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(AcceptedResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody] UpdateProcessCommandDTO dto) =>
            await SendAsync(new UpdateProcessCommand(id, dto.Name, dto.Version));

        //HTTPDELETE

    }

}