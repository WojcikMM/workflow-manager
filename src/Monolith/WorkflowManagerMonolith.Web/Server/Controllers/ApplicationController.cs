﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowManagerMonolith.Application.Applications;
using WorkflowManagerMonolith.Application.Applications.DTOs;

namespace WorkflowManagerMonolith.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetApplicationsQuery query)
        {
            var result = await applicationService.BrowseApplicationsAsync(query);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var result = await applicationService.GetApplicationByIdAsync(Id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateApplicationCommand command)
        {
            await applicationService.CreateApplicationAsync(command);
            return CreatedAtAction(nameof(this.GetById), new { Id = command.ApplicationId });
        }

        [HttpPatch("{Id}/assign/{UserId}")]
        public async Task<IActionResult> AssignApplication([FromRoute] Guid Id, [FromRoute] Guid UserId)
        {
            var command = new AssignUserHandlingCommand()
            {
                ApplicationId = Id,
                UserId = UserId
            };
            await applicationService.AssignUserHandling(command);
            return NoContent();
        }

        [HttpPatch("{Id}/release")]
        public async Task<IActionResult> ReleaseHandling([FromRoute] Guid Id)
        {
            var command = new ReleaseUserHandlingCommand()
            {
                ApplicationId = Id
            };

            await applicationService.ReleaseUserHandling(command);
            return NoContent();
        }

        [HttpPost("{Id}/apply/{TransactionId}")]
        public async Task<IActionResult> ApplyTransaction([FromRoute] Guid Id, [FromRoute] Guid TransactionId)
        {
            var command = new ApplyTransactionCommand()
            {
                ApplicationId = Id,
                TransactionId = TransactionId,
                UserId = new Guid()
            };

            await applicationService.ApplyTransaction(command);

            return NoContent();
        }
    }
}