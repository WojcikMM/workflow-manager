using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.ProcessesService.API.DTO.Commands;
using WorkflowManager.ProcessesService.ReadModel.ReadDatabase;
using WorkflowManager.Common.Controllers;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.Common.ApiResponses;
using Microsoft.AspNetCore.Authorization;

namespace WorkflowManager.ProcessesService.API.Controllers
{
    [Authorize]
    public class ProcessesController : BaseController
    {
        private readonly IReadModelRepository<ProcessModel> _readModelRepository;

        public ProcessesController(IBusPublisher busPublisher, IReadModelRepository<ProcessModel> readModelRepository): base(busPublisher)
        {
            _readModelRepository = readModelRepository ?? 
                throw new ArgumentNullException(nameof(readModelRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProcessModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProcesses([FromQuery]string name = null) => 
            Collection(await _readModelRepository.SearchAsync(name));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProcessModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProcess([FromRoute]Guid id) => 
            Single(await _readModelRepository.GetByIdAsync(id));


        [HttpPost]
        [Authorize(Roles = "processes_manager")]
        public async Task<IActionResult> CreateProcess([FromBody] CreateProcessDTOCommand command) =>
            await SendAsync(new CreateProcessCommand(Guid.NewGuid(), command.Name));

        [HttpPatch("{id}")]
        [Authorize(Roles = "processes_manager")]
        public async Task<IActionResult> UpdateProcess([FromRoute]Guid id, UpdateProcessDTOCommand command) =>
           await SendAsync(new UpdateProcessCommand(id, command.Name, command.Version));
    }
}