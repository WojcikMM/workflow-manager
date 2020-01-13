using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Rejected
{
    public class ProcessCreateRejectedEvent : BaseRejectedEvent
    {
        public ProcessCreateRejectedEvent() : base("Could not create process with given id.")
        {
        }
    }
}
