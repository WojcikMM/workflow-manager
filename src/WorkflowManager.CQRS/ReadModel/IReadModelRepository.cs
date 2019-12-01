using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkflowManager.CQRS.ReadModel
{
    public interface IReadModelRepository<TReadModel> where TReadModel : IReadModel, new()
    {
        Task<IEnumerable<TReadModel>> GetAllAsync();
        Task<IEnumerable<TReadModel>> SearchAsync(string query);
        Task<TReadModel> GetByIdAsync(Guid id);

        Task AddAsync(TReadModel model);
        Task UpdateAsync(TReadModel model);
        Task RemoveAsync(Guid id);
    }
}
