using BLL.Interfaces;
using DAL.EF;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Implementation
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;
        private DbSet<User> _dbSet;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task Create(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _dbSet.FindAsync(id);

            if (user != null)
            {
                _dbSet.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User> Get(int id, bool includeRoles = false)
        {
            if (includeRoles)
                _dbSet.Include(d => d.UserRoles)
                    .ThenInclude(d => d.Role).ThenInclude(d => d.RoleName)
                    .AsNoTracking();
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAll(bool includeRoles = false, bool sorted = false)
        {
            if (includeRoles)
                _dbSet.Include(d => d.UserRoles)
                    .ThenInclude(d => d.Role)
                    .AsNoTracking();
            if (sorted)
                _dbSet.OrderBy(d => d.Id).AsNoTracking();
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task Update(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesById(int id)
        {
            return await _context.Roles.Where(u => u.Id == id).AsNoTracking().ToListAsync(); ;
        }
    }
}
