using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Statuses;
using WorkflowManagerMonolith.Application.Statuses.DTOs;
using WorkflowManagerMonolith.Web.Server.Dtos;

namespace WorkflowManagerMonolith.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : BaseController
    {
        private readonly IStatusesService statusesService;

        public StatusesController(IStatusesService statusesService)
        {
            this.statusesService = statusesService;
        }

        [HttpGet]
        public async Task<IActionResult> BrowseStatuses([FromQuery] GetStatusesQuery query)
        {
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
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusCommand command)
        {
            await statusesService.CreateStatusAsync(command);
            return Created($"/api/statuses/{command.Id}", new EntityCreatedDto { Id = command.Id });
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid Id, [FromBody] UpdateStatusCommand command)
        {
            await statusesService.UpdateStatusAsync(Id, command);
            return NoContent();
        }
    }
}
