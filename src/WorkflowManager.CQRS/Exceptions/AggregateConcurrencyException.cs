using System;

namespace WorkflowManager.CQRS.Domain.Exceptions
{
    public class AggregateConcurrencyException : Exception
    {
        public AggregateConcurrencyException()
        {
        }
        public AggregateConcurrencyException(string message) : base(message)
        {
        }

        public AggregateConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
