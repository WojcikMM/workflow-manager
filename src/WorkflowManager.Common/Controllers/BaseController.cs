﻿using WorkflowManager.CQRS.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WorkflowManager.Common.RabbitMq;
using System.Net;
using WorkflowManager.Common.ApiResponses;
using System.Collections.Generic;

namespace WorkflowManager.Common.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        private readonly IBusPublisher _busPublisher;

        protected BaseController(IBusPublisher busPublisher)
        {
            _busPublisher = busPublisher;
        }

        protected IActionResult Single<T>(T model, Func<T, bool> criteria = null)
        {
            bool isValid = model != null && (criteria == null || criteria(model));
            if (isValid)
            {
                return Ok(model);
            }

            return NotFound(new NotFoundResponse());
        }

        protected IActionResult Collection<T>(IEnumerable<T> results)
        {
            return Ok(results ?? new List<T>());
        }

        protected IActionResult Collection<T>(T pagedResult, Func<T, bool> criteria)
        {
            if (pagedResult == null)
            {
                return NotFound();
            }
            bool isValid = criteria == null || criteria(pagedResult);
            if (!isValid)
            {
                return NotFound();
            }
            //if (pagedResult.IsEmpty)
            //{
            //    return Ok(Enumerable.Empty<T>());
            //}
            //Response.Headers.Add("Link", GetLinkHeader(pagedResult));
            //Response.Headers.Add("X-Total-Count", pagedResult.TotalResults.ToString());

            //return Ok(pagedResult.Items);

            return Ok(pagedResult);
        }


        protected async Task<IActionResult> SendAsync<T>(T command) where T : ICommand
        {
            //var context = GetContext<T>(resourceId, resource);
            Guid correlationId = Guid.NewGuid();
            await _busPublisher.SendAsync(command, correlationId);

            return Accepted(new
            {
                AggregateId = command.Id,
                CorrelationId = correlationId
            });
        }
        protected bool IsAdmin
            => User.IsInRole("admin");

        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ?
                Guid.Empty :
                Guid.Parse(User.Identity.Name);
    }
}