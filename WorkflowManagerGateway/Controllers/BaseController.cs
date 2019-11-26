using System;
using System.Threading.Tasks;
using CQRS.Template.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using WorkflowManager.Common.RabbitMq;

namespace WorkflowManagerGateway.Controllers
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
            if (model == null)
            {
                return NotFound();
            }
            var isValid = criteria == null || criteria(model);
            if (isValid)
            {
                return Ok(model);
            }

            return NotFound();
        }

        protected IActionResult Collection<T>(T pagedResult, Func<T, bool> criteria = null)
        {
            if (pagedResult == null)
            {
                return NotFound();
            }
            var isValid = criteria == null || criteria(pagedResult);
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


        protected async Task<IActionResult> SendAsync<T>(T command,Guid? resourceId = null, string resource = "") where T : ICommand
        {
            //var context = GetContext<T>(resourceId, resource);
            var correlationId = Guid.NewGuid();
            await _busPublisher.SendAsync(command, correlationId);

            return Accepted(new
            {
                CorrelationId = correlationId
            });
        }

        //protected IActionResult Accepted(ICorrelationContext context)
        //{
        //    Response.Headers.Add(OperationHeader, $"operations/{context.Id}");
        //    if (!string.IsNullOrWhiteSpace(context.Resource))
        //    {
        //        Response.Headers.Add(ResourceHeader, context.Resource);
        //    }

        //    return base.Accepted();
        //}

        //protected ICorrelationContext GetContext<T>(Guid? resourceId = null, string resource = "") where T : ICommand
        //{
        //    if (!string.IsNullOrWhiteSpace(resource))
        //    {
        //        resource = $"{resource}/{resourceId}";
        //    }

        //    return CorrelationContext.Create<T>(Guid.NewGuid(), UserId, resourceId ?? Guid.Empty,
        //       HttpContext.TraceIdentifier, HttpContext.Connection.Id, _tracer.ActiveSpan.Context.ToString(),
        //       Request.Path.ToString(), Culture, resource);
        //}

        protected bool IsAdmin
            => User.IsInRole("admin");

        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ?
                Guid.Empty :
                Guid.Parse(User.Identity.Name);

        //protected string Culture
        //    => Request.Headers.ContainsKey(AcceptLanguageHeader) ?
        //            Request.Headers[AcceptLanguageHeader].First().ToLowerInvariant() :
        //            DefaultCulture;

        //private string GetLinkHeader(PagedResultBase result)
        //{
        //    var first = GetPageLink(result.CurrentPage, 1);
        //    var last = GetPageLink(result.CurrentPage, result.TotalPages);
        //    var prev = string.Empty;
        //    var next = string.Empty;
        //    if (result.CurrentPage > 1 && result.CurrentPage <= result.TotalPages)
        //    {
        //        prev = GetPageLink(result.CurrentPage, result.CurrentPage - 1);
        //    }
        //    if (result.CurrentPage < result.TotalPages)
        //    {
        //        next = GetPageLink(result.CurrentPage, result.CurrentPage + 1);
        //    }

        //    return $"{FormatLink(next, "next")}{FormatLink(last, "last")}" +
        //           $"{FormatLink(first, "first")}{FormatLink(prev, "prev")}";
        //}

        //private string GetPageLink(int currentPage, int page)
        //{
        //    var path = Request.Path.HasValue ? Request.Path.ToString() : string.Empty;
        //    var queryString = Request.QueryString.HasValue ? Request.QueryString.ToString() : string.Empty;
        //    var conjunction = string.IsNullOrWhiteSpace(queryString) ? "?" : "&";
        //    var fullPath = $"{path}{queryString}";
        //    var pageArg = $"{PageLink}={page}";
        //    var link = fullPath.Contains($"{PageLink}=")
        //        ? fullPath.Replace($"{PageLink}={currentPage}", pageArg)
        //        : fullPath += $"{conjunction}{pageArg}";

        //    return link;
        //}

        private static string FormatLink(string path, string rel)
            => string.IsNullOrWhiteSpace(path) ? string.Empty : $"<{path}>; rel=\"{rel}\",";
    }
}