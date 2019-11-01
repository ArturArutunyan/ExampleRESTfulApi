using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExampleRESTfulApi.Controllers.Administration
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RolesController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Все роли
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ApplicationRole> GetAll() => _roleManager.Roles.ToList();

        //[HttpGet("{guid}")]
        //public async Task<IActionResult> Get(Guid guid)
        //{
        //    var role = await _roleManager.FindByIdAsync(guid.ToString());

        //    if (role != null)
        //        return Ok(role);
        //    return NotFound();
        //}

        /// <summary>
        /// Создать роль
        /// </summary>
        /// <param name="name">Имя роли</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            var role = new ApplicationRole() { Name = name };

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
                return Ok(role);
            return Conflict();
        }

        /// <summary>
        /// Удалить роль
        /// </summary>
        /// <param name="name">Имя роли</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
                return Ok();
            }
            return NotFound();
        }


        /// <summary>
        /// Добавление ролей пользователю
        /// </summary>
        /// <param name="guid">guid полизователя</param>
        /// <param name="roles">Список активных и неактивных ролей</param>
        /// <returns></returns>
        [HttpPost("{guid}/roles")]
        public async Task<IActionResult> ChangeRole(Guid guid, [FromBody] IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var addedRoles = roles.Except(userRoles);

                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return Ok(user);
            }
            return NotFound();
        }
    }
}