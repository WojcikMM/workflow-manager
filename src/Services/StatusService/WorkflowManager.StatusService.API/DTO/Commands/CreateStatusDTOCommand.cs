using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowManager.StatusService.API.DTO.Commands
{
    public class CreateStatusDTOCommand
    {
        public string Name { get; set; }
        public Guid ProcessId { get; set; }
    }
}
