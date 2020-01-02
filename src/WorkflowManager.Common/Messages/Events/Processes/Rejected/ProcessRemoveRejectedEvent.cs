using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Rejected
{
    public class ProcessRemoveRejectedEvent : BaseRejectedEvent
    {
        public ProcessRemoveRejectedEvent() : base("Could not remove process with given id.")
        {
        }
    }

}
