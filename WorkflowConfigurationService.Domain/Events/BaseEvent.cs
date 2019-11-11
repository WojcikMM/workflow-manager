using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowConfigurationService.Domain.Events
{
    public class BaseEvent:IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public Guid AggregateId { get; set; }
    }
}
