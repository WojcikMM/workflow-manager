using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.ConfigurationService.API.DTO.Commands;
using WorkflowManager.Common.Controllers;
using WorkflowManager.Common.ApiResponses;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using MassTransit;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;
using WorkflowManager.ConfigurationService.API.DTO.Query;

namespace WorkflowManager.ConfigurationService.API.Controllers
{
    [Authorize]
    public class ProcessesController : BaseWithPublisherController
    {
        private readonly IMapper _mapper;
        private readonly IReadModelRepository<ProcessModel> _readModelRepository;

        public ProcessesController(IReadModelRepository<ProcessModel> readModelRepository, IPublishEndpoint publishEndpoint, IMapper mapper) : base(publishEndpoint)
        {
            this._mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _readModelRepository = readModelRepository ??
                throw new ArgumentNullException(nameof(readModelRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProcessDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProcesses([FromQuery]string name = null) =>
            Collection(_mapper.Map<IEnumerable<ProcessDto>>(await _readModelRepository.SearchAsync(name)));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProcessDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProcess([FromRoute]Guid id) =>
            Single(_mapper.Map<ProcessDto>(await _readModelRepository.GetByIdAsync(id)));

        [HttpPost]
        [Authorize(Roles = "processes_manager")]
        public async Task<IActionResult> CreateProcess([FromBody] CreateProcessCommandDto command) =>
            await SendAsync(new CreateProcessCommand(command.Name));

        [HttpPatch("{id}")]
        [Authorize(Roles = "processes_manager")]
        public async Task<IActionResult> UpdateProcess([FromRoute]Guid id, UpdateProcessCommandDto command) =>
           await SendAsync(new UpdateProcessCommand(id, command.Name, command.Version));
    }
}