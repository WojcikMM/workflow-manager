using System;

namespace CQRS.Template.Domain.Exceptions
{
    public class UnregisteredDomainCommandException : Exception
    {
        public UnregisteredDomainCommandException()
        {
        }
        public UnregisteredDomainCommandException(string message) : base(message)
        {
        }

        public UnregisteredDomainCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}
