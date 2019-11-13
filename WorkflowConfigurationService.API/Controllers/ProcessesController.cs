using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkflowConfiguration.Infrastructure.Commands;
using WorkflowConfigurationService.Domain.Bus;
using WorkflowConfigurationService.Infrastructure.ApiCommand;

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
        public async Task<IActionResult> CreateProcess([FromBody] CreateProcessApiCommand createProcessCommand)
        {
            var newProcessId = Guid.NewGuid();
            await _commandBus.Send(new CreateProcessCommand(newProcessId, createProcessCommand.Name));
            return Accepted($"/api/processes/{newProcessId}");
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> UpdateProcess([FromRoute]Guid Id, [FromBody] UpdateProcessApiCommand updateProcessApiCommand)
        {
            await _commandBus.Send(new UpdateProcessCommand(Id, updateProcessApiCommand.Name, updateProcessApiCommand.Version));
            return Accepted($"/api/processes/{Id}");
        }
    }
}