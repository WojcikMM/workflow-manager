using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CQRS.Template.Domain.Bus;
using CQRS.Template.ReadModel;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.ProcessService.API.DTO.Commands;
using WorkflowManager.ProcessService.API.DTO.ErrorResponses;
using WorkflowManager.ProcessService.API.DTO.Responses;
using WorkflowManager.ProductService.Core.Commands;
using WorkflowManager.ProductService.Core.ReadModel.Models;

namespace WorkflowManager.ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(InternalServerErrorResponse),(int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(SimpleErrorResponse),(int)HttpStatusCode.Conflict)]
    public class ProcessController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IReadModelRepository<ProcessReadModel> _readModelRepository;

        public ProcessController(ICommandBus commandBus, IReadModelRepository<ProcessReadModel> readModelRepository)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _readModelRepository = readModelRepository ?? throw new ArgumentNullException(nameof(readModelRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProcessReadModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProcesses() => Ok(await _readModelRepository.GetAll());

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProcessReadModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProcess(Guid id) => Ok(await _readModelRepository.GetById(id));


        [HttpPost]
        [ProducesResponseType(typeof(AcceptedResponseDTO), (int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CreateProcess([FromBody] CreateProcessDTOCommand createProcessCommand)
        {
            var response = new AcceptedResponseDTO();
            await _commandBus.Send(new CreateProcessCommand(response.ProductId, createProcessCommand.Name), response.CorrelationId);
            return Accepted($"/api/processes/{response.ProductId}", response);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(typeof(CorrelationIdResponseDTO), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(NotFoundErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateProcess([FromRoute]Guid Id, [FromBody] UpdateProcessDTOCommand updateProcessApiCommand)
        {
            var correlationResponse = new CorrelationIdResponseDTO();
            await _commandBus.Send(new UpdateProcessCommand(Id, updateProcessApiCommand.Name, updateProcessApiCommand.Version), correlationResponse.CorrelationId);
            return Accepted($"/api/processes/{Id}", correlationResponse);
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(CorrelationIdResponseDTO), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(NotFoundErrorResponse),(int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RemoveProcess([FromRoute]Guid Id)
        {
            var correlationResponse = new CorrelationIdResponseDTO();
            var process = await _readModelRepository.GetById(Id);
            await _commandBus.Send(new RemoveProcessCommand(Id, process.Version), correlationResponse.CorrelationId);
            return Accepted(correlationResponse);
        }


    }
}