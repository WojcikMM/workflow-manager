using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WorkflowManager.CQRS.ReadModel;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase;
using WorkflowManager.ConfigurationService.ReadModel.ReadDatabase.Models;

namespace WorkflowManager.ConfigurationService.ReadModel.Repositories
{
    public class TransactionReadModelRepository : IReadModelRepository<TransactionModel>
    {
        private readonly ConfigurationDbContext _context;

        public TransactionReadModelRepository(ConfigurationDbContext context) => _context = context;

        public async Task AddAsync(TransactionModel model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionModel>> SearchAsync(string query)
        {
            IQueryable<TransactionModel> transactions = _context.Transactions.AsQueryable().Include(x => x.Status).Include(x=> x.OutgoingStatus);
            if (!string.IsNullOrWhiteSpace(query))
            {
                transactions = transactions
                    .Where(m => m.Name.ToLower().Contains(query.ToLower()));
            }

            return await transactions.ToListAsync();
        }

        public async Task<IEnumerable<TransactionModel>> GetAllAsync() =>
           await SearchAsync(null);

        public async Task<TransactionModel> GetByIdAsync(Guid id) =>
            await _context.Transactions.FindAsync(id);

        public async Task RemoveAsync(Guid id)
        {
            TransactionModel transaction = await GetByIdAsync(id);
            _context.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TransactionModel model)
        {
            TransactionModel transaction = await GetByIdAsync(model.Id);
            transaction.Name = model.Name;
            transaction.Version = model.Version;
            transaction.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Guid id)
        {
            return _context.Transactions.AnyAsync(x => x.Id == id);
        }
    }
}
