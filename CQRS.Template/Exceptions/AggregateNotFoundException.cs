using System;

namespace CQRS.Template.Domain.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException()
        {
        }
        public AggregateNotFoundException(string message) : base(message)
        {
        }

        public AggregateNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
