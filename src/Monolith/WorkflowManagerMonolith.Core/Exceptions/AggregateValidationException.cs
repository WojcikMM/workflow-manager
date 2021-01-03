using System;

namespace WorkflowManagerMonolith.Core.Exceptions
{
    public class AggregateValidationException : Exception
    {
        public AggregateValidationException(string message) : base(message)
        {
        }

        public AggregateValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
