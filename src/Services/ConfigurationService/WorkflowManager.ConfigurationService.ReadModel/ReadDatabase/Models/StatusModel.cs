using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WorkflowManager.CQRS.ReadModel;

namespace WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models
{
    public class StatusModel : IReadModel
    {
        [Column("StatusId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }

        public Guid ProcessId { get; set; }
        public ProcessModel Process { get; set; }

        //TODO: CHECK IF IT WILL WORK WITHOUT DEFINE FOREIGN KEYS IMPLICIT
        public ICollection<TransactionModel> Transactions { get; set; }
        public ICollection<TransactionModel> OutgoingTransactions { get; set; }
    }
}
