using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WorkflowManager.Common.ApiResponses;
using WorkflowManager.Common.Controllers;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.Common.RabbitMq;
using WorkflowManagerGateway.Commands;
using WorkflowManagerGateway.DTOs;
using WorkflowManagerGateway.Services;

namespace WorkflowManagerGateway.Controllers
{
    public class ProcessesController : BaseController
    {
        private readonly IProcessesService _processesService;

        public ProcessesController(IProcessesService processesService, IBusPublisher busPublisher) : base(busPublisher)
        {
            _processesService = processesService ?? throw new ArgumentNullException(nameof(processesService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProcessDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] string name = null)
            => Collection(await _processesService.BrowseAsync(name));


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProcessDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
         => Single(await _processesService.GetAsync(id));


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