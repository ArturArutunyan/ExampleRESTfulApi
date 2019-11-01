
using BLL.Entities.Responses;
using BLL.Entities.Templates.Identity;
using BLL.Interfaces.Identity;
using DAL.DAO.EF;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Managers.Identity
{
    public class Users : IUsers
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public Users(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public PagedResponse<User> GetAllAsync()
        {
            var usersList = _context.Users
                .ToList();

            var rolesQuery = from u in _context.Users                                   // cоединяем таблицы Users и Roles
                             join ur in _context.UserRoles on u.Id equals ur.UserId     // используя промежуточную таблицу
                             join r in _context.Roles on ur.RoleId equals r.Id
                             select new                                                 // формируем список ролей сожердащих UserId пользователя
                             {
                                 ur.UserId,
                                 r.Id,
                                 r.Name
                             };
            
            var users = usersList.Select(x => new User()            // итоговый список 
            {
                Id = x.Id,
                Login = x.UserName,
                Roles = rolesQuery.Where(r => r.UserId == x.Id)     // по UserId ищем роли пользователя и для каждого пользователя формируем массив его ролей
                .Select(s => new Role()
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToArray()
            });


            return new PagedResponse<User>() { Data = users };
        }

        public async Task<User> GetAsyncById(Guid guid)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());           
            
            if (user != null)
            {
                var rolesQuery = from r in _context.Roles
                                 join ur in _context.UserRoles on r.Id equals ur.RoleId
                                 where ur.UserId == user.Id
                                 select new
                                 {
                                     r.Id,
                                     r.Name
                                 };

                return new User()
                {
                    Id = user.Id,
                    Login = user.UserName,
                    Email = user.Email,
                    Roles = rolesQuery.Select(s => new Role()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToArray()
                };
            }

            return null;
        }

        public async Task<bool> CreateAsync(User model)
        {
            ApplicationUser user = model;

            var result = await _userManager.CreateAsync(user, "111111"); // cтавим дефолтный пароль

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateAsync(User model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
           
            if (user != null)
            {
                user.UserName = model.Login;
                user.Email = model.Email;

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAsync(Guid guid)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());

            if (user != null)
            {
                await _userManager.DeleteAsync(user);            
                return true;
            }              
            return false;
        }

        public async Task<IEnumerable<string>> GetAllUserRolesAsync(Guid guid)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                return userRoles;
            }
            return null;
        }

        public async Task<User> ResetPasswordAsync(Guid guid)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());           

            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, resetToken, "111111");
                User model = (User)user; 

                return model;
            }
            return null;
        }
    }
}
