using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.Common.ApiResponses;
using WorkflowManager.Common.Controllers;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.ConfigurationService.API.DTO.Commands;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;
using WorkflowManager.ConfigurationService.API.DTO.Query;

namespace WorkflowManager.ConfigurationService.API.Controllers
{
    public class StatusesController : BaseWithPublisherController
    {
        private readonly IMapper _mapper;
        private readonly IReadModelRepository<StatusModel> _readModelRepository;

        public StatusesController(IPublishEndpoint publishEndpoint, IReadModelRepository<StatusModel> readModelRepository, IMapper mapper) : base(publishEndpoint)
        {
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _readModelRepository = readModelRepository ??
                throw new ArgumentNullException(nameof(readModelRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StatusDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetStatuses([FromQuery]string name = "") =>
            Collection(_mapper.Map<IEnumerable<StatusModel>,IEnumerable<StatusDto>>(await _readModelRepository.SearchAsync(name)));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StatusDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetStatus([FromRoute]Guid id) =>
            Single(_mapper.Map<StatusDto>(await _readModelRepository.GetByIdAsync(id)));

        [HttpPost]
        [Authorize(Roles = "statuses_manager")]
        [ProducesResponseType(typeof(AcceptedResponse), (int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDTOCommand command) =>
            await SendAsync(new CreateStatusCommand(command.Name, command.ProcessId));

        [HttpPatch("{id}")]
        [Authorize(Roles = "statuses_manager")]
        [ProducesResponseType(typeof(AcceptedResponse), (int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> UpdateStatus([FromRoute]Guid id, UpdateStatusDTOCommand command) =>
            await SendAsync(new UpdateStatusCommand(id, command.Name, command.ProcessId, command.Version));
    }
}