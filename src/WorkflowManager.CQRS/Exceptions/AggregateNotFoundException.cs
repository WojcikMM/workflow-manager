﻿using System;

namespace WorkflowManager.CQRS.Domain.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException() : base() { }
        public AggregateNotFoundException(string message) : base(message) { }
        public AggregateNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

}
