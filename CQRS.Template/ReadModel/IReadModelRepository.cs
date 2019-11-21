using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Template.ReadModel
{
    public interface IReadModelRepository<TReadModel> where TReadModel : IReadModel, new()
    {
        Task<IEnumerable<TReadModel>> GetAll();
        Task<TReadModel> GetById(Guid id);

        Task Add(TReadModel model);
        Task Update(TReadModel model);
        Task Remove(Guid id);
    }
}
