
using BLL.Interfaces;
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
    public class RoleRepository : IRoleRepository
    {
        private ApplicationDbContext _context;
        private DbSet<Role> _dbSet;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Role>();
        }

        public async Task Create(Role role)
        {
            await _context.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var role = await _dbSet.FindAsync(id);

            if (role != null)
            {
                _dbSet.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Role> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task Update(Role role)
        {
            _context.Update(role);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Role> GetWithInclude(params Expression<Func<Role, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<Role> GetWithInclude(Func<Role, bool> predicate,
            params Expression<Func<Role, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<Role> Include(params Expression<Func<Role, object>>[] includeProperties)
        {
            IQueryable<Role> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
