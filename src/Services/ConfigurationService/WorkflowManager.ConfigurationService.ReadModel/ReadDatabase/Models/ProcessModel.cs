using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WorkflowManager.CQRS.ReadModel;

namespace WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models
{
    public class ProcessModel : IReadModel
    {
        [Column("ProcessId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }

        public ICollection<StatusModel> Statuses { get; set; }
    }
}
