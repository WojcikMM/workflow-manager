using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;
using System.Security.Cryptography.X509Certificates;

namespace WorkflowManager.ConfigurationService.ReadModel.Repositories
{
    public class StatusReadModelRepository : IReadModelRepository<StatusModel>
    {
        private readonly ConfigurationDbContext _context;

        public StatusReadModelRepository(ConfigurationDbContext context) => _context = context;

        public async Task AddAsync(StatusModel model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<StatusModel>> SearchAsync(string query)
        {
            IQueryable<StatusModel> processes = _context.Statuses.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                processes = processes
                    .Where(m => m.Name.ToLower().Contains(query.ToLower()));
            }

            return await processes.ToListAsync();
        }

        public async Task<IEnumerable<StatusModel>> GetAllAsync() =>
           await SearchAsync(null);

        public async Task<StatusModel> GetByIdAsync(Guid id) =>
            await _context.Statuses.FindAsync(id);

        public async Task RemoveAsync(Guid id)
        {
            StatusModel process = await GetByIdAsync(id);
            _context.Remove(process);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StatusModel model)
        {
            StatusModel process = await GetByIdAsync(model.Id);
            process.Name = model.Name;
            process.Version = model.Version;
            process.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Guid id)
        {
            return _context.Statuses.AnyAsync(x => x.Id == id);
        }
    }
}
