using System;

namespace WorkflowManagerMonolith.Application.Applications.DTOs
{
    public class AssignUserHandlingCommand
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
    }
}
