using System;
using System.Net;
using System.Threading.Tasks;
using CQRS.Template.Domain.Bus;
using CQRS.Template.ReadModel;
using Microsoft.AspNetCore.Mvc;
using WorkflowConfigurationService.API.DTOCommands;
using WorkflowConfigurationService.Core.Processes.Commands;
using WorkflowConfigurationService.Core.ReadModel.Models;

namespace WorkflowConfigurationService.API.Controllers
{
    public class ProcessesController : BaseController<ProcessReadModel>
    {
        public ProcessesController(ICommandBus commandBus, IReadModelRepository<ProcessReadModel> readModelRepository) : base(commandBus, readModelRepository)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProcessReadModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProcesses() => await HandleGetAllRequest();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProcessReadModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProcess(Guid id) => await HandleGetByIdRequest(id);


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CreateProcess([FromBody] CreateProcessDTOCommand createProcessCommand)
        {
            var newProcessId = Guid.NewGuid();
            await _commandBus.Send(new CreateProcessCommand(newProcessId, createProcessCommand.Name));
            return Accepted($"/api/processes/{newProcessId}");
        }

        [HttpPut("{Id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> UpdateProcess([FromRoute]Guid Id, [FromBody] UpdateProcessDTOCommand updateProcessApiCommand)
        {
            await _commandBus.Send(new UpdateProcessCommand(Id, updateProcessApiCommand.Name, updateProcessApiCommand.Version));
            return Accepted($"/api/processes/{Id}");
        }
    }
}