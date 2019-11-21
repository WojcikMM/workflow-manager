using System;
using CQRS.Template.Domain.Commands;

namespace WorkflowManager.ProductService.Core.Commands
{
    public class UpdateProcessCommand : BaseCommand
    {
        public string Name { get; set; }
        public UpdateProcessCommand(Guid Id, string Name, int Version) : base(Id, Version)
        {
            this.Name = Name;
        }
    }
}
