using System;
using WorkflowManagerMonolith.Core.Abstractions;

namespace WorkflowManagerMonolith.Core.Commands
{
    public class GetApplicationCommand : ICommand
    {
        public Guid ApplicationId { get; set; }
    }
}
