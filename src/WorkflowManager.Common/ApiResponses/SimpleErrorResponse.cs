using System;

namespace WorkflowManager.Common.ApiResponses
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
