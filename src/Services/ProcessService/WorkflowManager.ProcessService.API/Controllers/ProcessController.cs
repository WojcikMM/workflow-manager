using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RawRabbit.vNext.Disposable;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.Common.ErrorResponses;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.ProcessService.API.DTO.Commands;
using WorkflowManager.ProcessService.ReadModel.ReadDatabase;
using WorkflowManager.Common.ApiResponses;

namespace WorkflowManager.ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(InternalServerErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(SimpleErrorResponse), (int)HttpStatusCode.Conflict)]
    public class ProcessController : ControllerBase
    {
        private readonly IBusClient _busClient;
        private readonly IReadModelRepository<ProcessModel> _readModelRepository;

        public ProcessController(IBusClient busClient, IReadModelRepository<ProcessModel> readModelRepository)
        {
            _busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
            _readModelRepository = readModelRepository ?? throw new ArgumentNullException(nameof(readModelRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProcessModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProcesses([FromQuery]string name = "") => Ok(await _readModelRepository.SearchAsync(name));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProcessModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProcess([FromRoute]Guid id) => Ok(await _readModelRepository.GetByIdAsync(id));


        [HttpPost]
        public async Task<IActionResult> CreateProcess([FromBody] CreateProcessDTOCommand dTOCommand)
        {
            AcceptedResponseDTO responseDTO = new AcceptedResponseDTO();
            CreateProcessCommand command = new CreateProcessCommand(responseDTO.ProductId, dTOCommand.Name);
            await _busClient.PublishAsync(command, responseDTO.CorrelationId);
            return Accepted(responseDTO);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProcess([FromRoute]Guid id, UpdateProcessDTOCommand dTOCommand)
        {
            AcceptedResponseDTO responseDTO = new AcceptedResponseDTO(id);
            UpdateProcessCommand command = new UpdateProcessCommand(id, dTOCommand.Name, dTOCommand.Version);
            await _busClient.PublishAsync(command, responseDTO.CorrelationId);
            return Accepted(responseDTO);
        }
    }
}