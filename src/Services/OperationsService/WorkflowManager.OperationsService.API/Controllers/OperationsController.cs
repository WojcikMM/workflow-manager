using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.Common.ApiResponses;
using WorkflowManager.Common.Controllers;
using WorkflowManager.OperationsStorage.Api.Dto;
using WorkflowManager.OperationsStorage.Api.Services;

namespace WorkflowManager.OperationsStorage.Controllers
{
    public class OperationsController : BaseController
    {
        private readonly IOperationsStorage _operationsStorage;
        public OperationsController(IOperationsStorage operationsStorage) => _operationsStorage = operationsStorage;

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OperationDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id) =>
            Single(await _operationsStorage.GetAsync(id));
    }
}