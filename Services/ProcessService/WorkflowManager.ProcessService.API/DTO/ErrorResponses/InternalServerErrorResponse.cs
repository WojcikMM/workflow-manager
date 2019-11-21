using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowManager.ProcessService.API.DTO.ErrorResponses
{
    public class InternalServerErrorResponse
    {
        public string Message { get; set; }
        public string InternalMesage { get; set; }
        public string StackTrace { get; set; }
    }
}
