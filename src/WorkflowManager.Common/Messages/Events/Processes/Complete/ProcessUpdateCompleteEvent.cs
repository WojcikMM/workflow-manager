using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Complete
{
    public class ProcessUpdateCompleteEvent : BaseCompleteEvent
    {
        public ProcessUpdateCompleteEvent() : base("Process updated successfully.")
        {
        }
    }
}
