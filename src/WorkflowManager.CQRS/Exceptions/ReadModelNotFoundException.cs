using System;

namespace WorkflowManager.Common.Exceptions
{
    public class ReadModelNotFoundException : Exception
    {
        public ReadModelNotFoundException(string message) : base(message)
        {
        }

        public ReadModelNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
