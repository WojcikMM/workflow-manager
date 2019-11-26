using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Template.ReadModel
{
    public interface IReadModelRepository<TReadModel> where TReadModel : IReadModel, new()
    {
        Task<IEnumerable<TReadModel>> GetAll();
        Task<TReadModel> GetByIdAsync(Guid id);

        Task AddAsync(TReadModel model);
        Task Update(TReadModel model);
        Task Remove(Guid id);
    }
}
