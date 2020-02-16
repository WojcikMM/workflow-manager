using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.Common.ApiResponses;
using WorkflowManager.Common.Controllers;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.Common.RabbitMq;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.StatusService.API.DTO.Commands;
using WorkflowManager.StatusService.ReadModel.ReadDatabase;

namespace WorkflowManager.StatusService.API.Controllers
{
    public class StatusesController : BaseWithPublisherController
    {
        private readonly IReadModelRepository<StatusModel> _readModelRepository;

        public StatusesController(IBusPublisher busPublisher, IReadModelRepository<StatusModel> readModelRepository) : base(busPublisher)
        {
            _readModelRepository = readModelRepository ??
                throw new ArgumentNullException(nameof(readModelRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StatusModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStatuses([FromQuery]string name = "") =>
            Collection(await _readModelRepository.SearchAsync(name));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StatusModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetStatus([FromRoute]Guid id) => 
            Single(await _readModelRepository.GetByIdAsync(id));


        [HttpPost]
        [ProducesResponseType(typeof(AcceptedResponse),(int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDTOCommand command) =>
            await SendAsync(new CreateStatusCommand(Guid.NewGuid(), command.Name, command.ProcessId));

        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(AcceptedResponse), (int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> UpdateStatus([FromRoute]Guid id, UpdateStatusDTOCommand command) => await SendAsync(new UpdateStatusCommand(id, command.Name, command.ProcessId, command.Version));
    }
}