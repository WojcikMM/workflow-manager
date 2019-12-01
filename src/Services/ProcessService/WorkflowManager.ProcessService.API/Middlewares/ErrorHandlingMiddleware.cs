using WorkflowManager.CQRS.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WorkflowManager.Common.Exceptions;
using WorkflowManager.ProcessService.API.DTO.ErrorResponses;

namespace WorkflowManager.ProcessService.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ErrorHandlingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (ReadModelNotFoundException)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotFound);
            }
            catch (AggregateNotFoundException)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotFound);
            }
            catch (AggregateConcurrencyException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Conflict, new SimpleErrorResponse(ex.Message));
            }
            catch (UnregisteredDomainCommandException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotImplemented, new SimpleErrorResponse(ex.Message));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, new InternalServerErrorResponse
                {
                    Message = ex.Message,
                    InternalMesage = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode httpStatus, object responseType)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatus;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(responseType));
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode httpStatus)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatus;
            return context.Response.CompleteAsync();
        }
    }
}
