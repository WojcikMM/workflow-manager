using System;
using CQRS.Template.Domain.Events;

namespace WorkflowManager.ProductService.Core.Events
{
    public class ProcessRemovedEvent : BaseEvent
    {
        public ProcessRemovedEvent(Guid Id):base(Id)
        {

        }
    }
}
