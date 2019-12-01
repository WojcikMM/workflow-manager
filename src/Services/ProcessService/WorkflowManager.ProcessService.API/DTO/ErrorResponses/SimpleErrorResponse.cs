using System;

namespace WorkflowManager.ProcessService.API.DTO.ErrorResponses
{
    public class SimpleErrorResponse
    {
        public SimpleErrorResponse(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public string Message { get; set; }
    }
}
