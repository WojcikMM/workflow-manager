using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkflowManager.Common.Messages.Commands.Processes;
using WorkflowManager.Common.RabbitMq;
using WorkflowManagerGateway.Commands;
using WorkflowManagerGateway.Services;

namespace WorkflowManagerGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessesController : BaseController
    {
        private readonly IProcessesService _processesService;

        public ProcessesController(IProcessesService processesService, IBusPublisher busPublisher) : base(busPublisher)
        {
            _processesService = processesService ?? throw new ArgumentNullException(nameof(processesService));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string name = null)
            => Collection(await _processesService.BrowseAsync(name));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
         => Single(await _processesService.GetAsync(id));


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProcessCommandDTO createProcessCommandDTO) =>
            await SendAsync(new CreateProcessCommand(Guid.NewGuid(), createProcessCommandDTO.Name));


        [HttpPatch("{id}")]
        public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody] UpdateProcessCommandDTO updateProcessCommandDTO) =>
            await SendAsync(new UpdateProcessCommand(id, updateProcessCommandDTO.Name, updateProcessCommandDTO.Version));

        //HTTPDELETE

    }

}