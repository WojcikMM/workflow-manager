using System;
using CQRS.Template.Domain.Commands;

namespace WorkflowManager.ProductService.Core.Commands
{
    public class RemoveProcessCommand : BaseCommand
    {
        public RemoveProcessCommand(Guid Id, int Version):base(Id,Version)
        {

        }
    }
}
