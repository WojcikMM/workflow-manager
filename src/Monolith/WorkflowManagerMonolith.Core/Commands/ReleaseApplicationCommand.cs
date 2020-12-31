using System;
using WorkflowManagerMonolith.Core.Abstractions;

namespace WorkflowManagerMonolith.Core.Commands
{
    public class ReleaseApplicationCommand : ICommand
    {
        public Guid ApplicationId { get; set; }
    }
}
