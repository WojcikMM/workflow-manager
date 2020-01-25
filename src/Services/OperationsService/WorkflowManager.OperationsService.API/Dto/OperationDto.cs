using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowManager.OperationsStorage.Api.Dto
{
    public class OperationDto
    {
        public Guid AggregateId { get; set; }
        public string Status { get; set; }

    }
}
