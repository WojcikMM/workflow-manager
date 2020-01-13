using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Rejected
{
    public class ProcessUpdateRejectedEvent : BaseRejectedEvent
    {
        public ProcessUpdateRejectedEvent():base ("Could not update process with given id.")
        {
        }
    }

}
