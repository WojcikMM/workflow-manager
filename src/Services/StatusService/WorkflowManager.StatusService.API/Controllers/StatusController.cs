using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit.vNext.Disposable;
using WorkflowManager.Common.ApiResponses;
using WorkflowManager.Common.ErrorResponses;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.StatusService.API.DTO.Commands;
using WorkflowManager.StatusService.ReadModel.ReadDatabase;

namespace WorkflowManager.StatusService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(InternalServerErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(SimpleErrorResponse), (int)HttpStatusCode.Conflict)]
    public class StatusController : ControllerBase
    {
        private readonly IBusClient _busClient;
        private readonly IReadModelRepository<StatusModel> _readModelRepository;

        public StatusController(IBusClient busClient, IReadModelRepository<StatusModel> readModelRepository)
        {
            _busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
            _readModelRepository = readModelRepository ?? throw new ArgumentNullException(nameof(readModelRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StatusModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStatuses([FromQuery]string name = "") => Ok(await _readModelRepository.SearchAsync(name));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StatusModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetStatus([FromRoute]Guid id) => Ok(await _readModelRepository.GetByIdAsync(id));


        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDTOCommand dTOCommand)
        {
            AcceptedResponseDTO responseDTO = new AcceptedResponseDTO();
            CreateStatusCommand command = new CreateStatusCommand(responseDTO.ProductId, dTOCommand.Name,dTOCommand.ProcessId);
            await _busClient.PublishAsync(command, responseDTO.CorrelationId);
            return Accepted(responseDTO);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus([FromRoute]Guid id, UpdateStatusDTOCommand dTOCommand)
        {
            AcceptedResponseDTO responseDTO = new AcceptedResponseDTO(id);
            UpdateStatusCommand command = new UpdateStatusCommand(id, dTOCommand.Name, dTOCommand.ProcessId, dTOCommand.Version);
            await _busClient.PublishAsync(command, responseDTO.CorrelationId);
            return Accepted(responseDTO);
        }
    }
}