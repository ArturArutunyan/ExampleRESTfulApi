using BLL.Interfaces;
using BLL.Interfaces.Documents;
using DAL.EF;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.Implementation
{
    public class ContractDocumentRepository : IContractDocumentRepository
    {
        private ApplicationDbContext _context;
        private DbSet<ContractDocument> _dbSet;

        public ContractDocumentRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<ContractDocument>();
        }

        public async Task Create(ContractDocument contractDocument)
        {
            await _context.AddAsync(contractDocument);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var contractDocument = await _dbSet.FindAsync(id);

            if (contractDocument != null)
            {
                _dbSet.Remove(contractDocument);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ContractDocument> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<ContractDocument>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task Update(ContractDocument contractDocument)
        {
            _context.Update(contractDocument);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ContractDocument> GetWithInclude(params Expression<Func<ContractDocument, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<ContractDocument> GetWithInclude(Func<ContractDocument, bool> predicate,
            params Expression<Func<ContractDocument, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<ContractDocument> Include(params Expression<Func<ContractDocument, object>>[] includeProperties)
        {
            IQueryable<ContractDocument> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
