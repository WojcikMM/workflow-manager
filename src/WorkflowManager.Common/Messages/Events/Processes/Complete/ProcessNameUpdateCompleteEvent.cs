﻿using System;
using WorkflowManager.Common.Messages.Events.Saga;

namespace WorkflowManager.Common.Messages.Events.Processes.Complete
{
    public class ProcessNameUpdateCompleteEvent : BaseCompleteEvent
    {
        public ProcessNameUpdateCompleteEvent(Guid AggregateId) : base(AggregateId, "Process updated successfully.")
        {
        }
    }
}
