using System;
using System.Net;
using System.Threading.Tasks;
using CQRS.Template.Domain.Bus;
using Microsoft.AspNetCore.Mvc;
using WorkflowConfigurationService.API.DTOCommands;
using WorkflowConfigurationService.Core.Processes.Commands;

namespace WorkflowConfigurationService.API.Controllers
{
    public class ProcessesController : BaseController
    {
        private readonly ICommandBus _commandBus;

        public ProcessesController(ICommandBus commandBus)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        }

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