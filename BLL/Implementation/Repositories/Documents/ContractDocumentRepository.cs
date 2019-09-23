using BLL.Interfaces.Documents;
using DAL.EF;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ContractDocument> Get(int id, bool includeRoles = false)
        {
            if (includeRoles)
                _dbSet.Include(d => d.DocumentRoles)
                    .ThenInclude(d => d.Role)
                    .AsNoTracking();
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<ContractDocument>> GetAll(bool includeRoles = false, bool sorted = false)
        {
            if (includeRoles)
                _dbSet.Include(d => d.DocumentRoles)
                    .ThenInclude(d => d.Role)
                    .AsNoTracking();
            if (sorted)
                _dbSet.OrderBy(d => d.Id).AsNoTracking();
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task Update(ContractDocument contractDocument)
        {
            _context.Update(contractDocument);
            await _context.SaveChangesAsync();
        }
    }
}
