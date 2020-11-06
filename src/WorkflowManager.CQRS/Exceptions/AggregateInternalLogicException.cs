using System;

namespace WorkflowManager.CQRS.Domain.Exceptions
{
    public class AggregateInternalLogicException : Exception
    {
        public AggregateInternalLogicException() : base() { }
        public AggregateInternalLogicException(string message) : base(message) { }
        public AggregateInternalLogicException(string message, Exception innerException) : base(message, innerException) { }
    }

}
