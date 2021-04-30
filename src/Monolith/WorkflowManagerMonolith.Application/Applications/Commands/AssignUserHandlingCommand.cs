using System;

namespace WorkflowManagerMonolith.Application.Applications.Commands
{
    public class AssignUserHandlingCommand
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
    }
}
