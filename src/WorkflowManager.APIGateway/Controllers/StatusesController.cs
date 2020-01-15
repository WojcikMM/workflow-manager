using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkflowManager.Common.Messages.Commands.Statuses;
using WorkflowManager.Common.RabbitMq;
using WorkflowManagerGateway.Commands;
using WorkflowManagerGateway.Services;

namespace WorkflowManagerGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : BaseController
    {
        private readonly IStatusesService _statusesService;

        public StatusesController(IStatusesService statusesService, IBusPublisher busPublisher) : base(busPublisher)
        {
            _statusesService = statusesService ?? throw new ArgumentNullException(nameof(statusesService));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string name = null)
            => Collection(await _statusesService.BrowseAsync(name));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
         => Single(await _statusesService.GetAsync(id));


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStatusCommandDTO createStatusDto) =>
            await SendAsync(new CreateStatusCommand(Guid.NewGuid(), createStatusDto.Name, createStatusDto.ProcessId));


        [HttpPatch("{id}")]
        public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody] UpdateStatusCommandDTO updateStatusDto) =>
            await SendAsync(new UpdateStatusCommand(id, updateStatusDto.Name, updateStatusDto.ProcessId, updateStatusDto.Version));

        //HTTPDELETE

    }

}