using System;
using CQRS.Template.Domain.Commands;
using WorkflowManager.ProcessService.Core;

namespace WorkflowManager.ProductService.Core.Commands
{
    public class CreateProcessCommand : BaseCommand
    {
        public string Name { get; private set; }

        public CreateProcessCommand(Guid Id, string Name) : base(Id, DomainConstants.NewAggregateVersion)
        {
            this.Name = Name;
        }      
    }
}
