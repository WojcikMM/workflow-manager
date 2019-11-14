using System;
using System.Net;
using System.Threading.Tasks;
using CQRS.Template.Domain.Bus;
using CQRS.Template.ReadModel;
using Microsoft.AspNetCore.Mvc;
using WorkflowConfigurationService.API.DTO;
using WorkflowConfigurationService.API.DTOCommands;
using WorkflowConfigurationService.Core.Processes.Commands;
using WorkflowConfigurationService.Infrastructure.ReadModel.Models;

namespace WorkflowConfigurationService.API.Controllers
{
    public class ProcessesController : BaseController
    {
        private readonly ICommandBus _commandBus;
        private readonly IReadModelRepository<ProcessReadModel> _readModelRepository;

        public ProcessesController(ICommandBus commandBus, IReadModelRepository<ProcessReadModel> readModelRepository)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _readModelRepository = readModelRepository ?? throw new ArgumentNullException(nameof(readModelRepository));
        }

        // START -- candidates to Base Controller

        [HttpGet]
        [ProducesResponseType(typeof(ProcessDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProcesses()
        {
            var processes = await _readModelRepository.GetAll();
            return Ok(processes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProcessDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProcess(Guid id)
        {
            var process = await _readModelRepository.GetById(id);
            if(process is null)
            {
                return NotFound();
            }
            return Ok(process);
        }

        // END -- candidates to Base Controller

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