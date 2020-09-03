using System;
using WorkflowManager.CQRS.Domain.Commands;

namespace WorkflowManager.CQRS.Commands
{
    public class BaseCommand : ICommand
    {
        public Guid AggregateId { get; }

        public int Version { get; }

        public Guid CorrelationId { get; }

        /// <summary>
        /// This constructor should be used for creating new Command
        /// </summary>
        /// <param name="AggregateId">Identity of connected Aggregate</param>
        /// <param name="Version">Version of aggregate (for optymistic locking)</param>
        public BaseCommand(Guid AggregateId, int Version)
        {
            this.AggregateId = AggregateId;
            this.Version = Version;
            CorrelationId = Guid.NewGuid();
        }

        /// <summary>
        /// This constructor should be used to deserialize command from queue.
        /// </summary>
        /// <param name="AggregateId">Identity of connected Aggregate</param>
        /// <param name="Version">Version of aggregate (for optymistic locking)</param>
        /// <param name="CorrelationId">Identity for connect asynchronous flow of events</param>
        public BaseCommand(Guid AggregateId, int Version, Guid CorrelationId) : this(AggregateId, Version)
        {
            this.CorrelationId = CorrelationId;
        }
    }
}
