﻿using System;

namespace WorkflowManagerMonolith.Core.Exceptions
{
    public class AggregateIllegalLogicException : Exception
    {
        public AggregateIllegalLogicException(string message) : base(message)
        {
        }

        public AggregateIllegalLogicException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
