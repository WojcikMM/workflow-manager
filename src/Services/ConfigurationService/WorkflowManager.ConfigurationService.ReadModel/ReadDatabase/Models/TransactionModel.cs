using System;
using System.ComponentModel.DataAnnotations.Schema;
using WorkflowManager.CQRS.ReadModel;

namespace WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models
{
    public class TransactionModel : IReadModel
    {
        [Column("TransactionId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }

        public Guid StatusId { get; set; }
        public StatusModel Status { get; set; }

        public Guid OutgoingStatusId { get; set; }
        public StatusModel OutgoingStatus { get; set; }
    }
}
