using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Statuses;
using WorkflowManagerMonolith.Application.Statuses.Commands;
using WorkflowManagerMonolith.Application.Statuses.Queries;
using WorkflowManagerMonolith.Web.Server.Controllers;
using WorkflowManagerMonolith.Web.Shared.Common;
using WorkflowManagerMonolith.Web.Shared.Statuses;

namespace WorkflowManagerMonolith.Web.Server.Domains.Statuses
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : BaseController
    {
        private readonly IStatusesService statusesService;
        private readonly IMapper mapper;

        public StatusesController(IStatusesService statusesService, IMapper mapper)
        {
            this.statusesService = statusesService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> BrowseStatuses([FromQuery] GetStatusesQueryDto getStatusesQuery)
        {
            var query = mapper.Map<GetStatusesQuery>(getStatusesQuery);
            var result = await statusesService.BrowseStatusesAsync(query);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStatusById([FromRoute] Guid Id)
        {
            var status = await statusesService.GetStatusById(Id);
            return Single(status);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDto createStatusDto)
        {
            var command = mapper.Map<CreateStatusCommand>(createStatusDto);
            await statusesService.CreateStatusAsync(command);
            return Created($"/api/statuses/{command.Id}", new EntityCreatedDto { Id = command.Id });
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid Id, [FromBody] UpdateStatusDto updateStatusDto)
        {
            var command = mapper.Map<UpdateStatusCommand>(updateStatusDto);
            await statusesService.UpdateStatusAsync(Id, command);
            return NoContent();
        }
    }
}
