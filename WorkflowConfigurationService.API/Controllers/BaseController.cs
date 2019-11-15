using System;
using System.Threading.Tasks;
using CQRS.Template.Domain.Bus;
using CQRS.Template.ReadModel;
using Microsoft.AspNetCore.Mvc;

namespace WorkflowConfigurationService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class BaseController<TReadModel> : ControllerBase where TReadModel : IReadModel, new()
    {
        protected readonly ICommandBus _commandBus;
        protected readonly IReadModelRepository<TReadModel> _readModelRepository;

        public BaseController(ICommandBus commandBus, IReadModelRepository<TReadModel> readModelRepository)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _readModelRepository = readModelRepository ?? throw new ArgumentNullException(nameof(readModelRepository));
        }


        protected async Task<IActionResult> HandleGetAllRequest()
        {
            var processes = await _readModelRepository.GetAll();
            return Ok(processes);
        }

        protected async Task<IActionResult> HandleGetByIdRequest(Guid id)
        {
            var process = await _readModelRepository.GetById(id);
            if (process is null)
            {
                return NotFound();
            }
            return Ok(process);
        }


    }
}