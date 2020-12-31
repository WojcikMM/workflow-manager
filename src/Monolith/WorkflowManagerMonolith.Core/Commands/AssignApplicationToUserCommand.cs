using System;
using WorkflowManagerMonolith.Core.Abstractions;

namespace WorkflowManagerMonolith.Core.Commands
{
    public class AssignApplicationToUserCommand : ICommand
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
    }
}
