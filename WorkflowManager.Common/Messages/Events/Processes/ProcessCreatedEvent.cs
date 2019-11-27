﻿using CQRS.Template.Domain.Events;
using System;

namespace WorkflowManager.Common.Messages.Events.Processes
{
    public class ProcessCreatedEvent : BaseEvent
    {
        public string Name { get; private set; }

        public ProcessCreatedEvent(Guid Id, string Name) : base(Id)
        {
            this.Name = Name;
        }

    }
}
