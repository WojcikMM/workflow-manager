using System;

namespace WorkflowManagerMonolith.Application.Applications.Commands
{
    public class CreateApplicationCommand
    {
        public Guid ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public Guid InitialTransactionId { get; set; }
        public Guid RegistrationUser { get; set; }
    }
}
