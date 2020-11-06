using System;

namespace WorkflowManager.CQRS.Domain.Exceptions
{
    public class UnregisteredDomainCommandException : Exception
    {
        public UnregisteredDomainCommandException() : base() { }
        public UnregisteredDomainCommandException(string message) : base(message) { }
        public UnregisteredDomainCommandException(string message, Exception innerException) : base(message, innerException) { }
    }

}
