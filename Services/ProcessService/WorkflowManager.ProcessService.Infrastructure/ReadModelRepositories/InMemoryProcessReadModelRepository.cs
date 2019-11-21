using WorkflowManager.ProductService.Core.ReadModel.Models;

namespace WorkflowManager.ProcessService.Infrastructure.ReadModelRepositories
{
    public class InMemoryProcessReadModelRepository : BaseInMemoryReadModelRepository<ProcessReadModel>
    {

        protected override void ModelUpdateMethod(ProcessReadModel currentReadModel, ProcessReadModel incomingReadModel)
        {
            currentReadModel.Name = incomingReadModel.Name;
            currentReadModel.Version = incomingReadModel.Version;
        }
    }
}
