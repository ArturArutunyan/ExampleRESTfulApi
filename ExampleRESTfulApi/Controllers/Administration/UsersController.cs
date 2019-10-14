using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExampleRESTfulApi.Controllers.api
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<ApplicationUser> GetAll() => _userManager.Users.ToList();

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());

            if (user != null)
                Ok(user);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, "111111"); // cтавим дефолтный пароль

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                return Created("api/users", model);
            }
            return Conflict();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] IdentityUser modelUser)
        {
            var user = await _userManager.FindByIdAsync(modelUser.Id);

            if (user != null)
            {
                user.UserName = modelUser.UserName;
                user.Email = modelUser.Email;

                await _userManager.UpdateAsync(user);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return Ok();
            return NotFound();
        }

        /// <summary>
        /// Сброс пароля
        /// </summary>
        /// <param name="guid">guid пользователя</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Route("{guid}/password")]
        public async Task<IActionResult> ResetPassword(Guid guid)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());

            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, resetToken, "111111");
                return Ok(user);
            }
            return NotFound();
        }

        /// <summary>
        /// Получение всех ролей пользователя
        /// </summary>
        /// <param name="guid">guid пользователя</param>
        /// <returns></returns>
        [HttpGet("{guid}/roles")]
        public async Task<IActionResult> GetAllUserRoles(Guid guid)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());

            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var allRoles = _roleManager.Roles.ToList();

                var roles = new
                {
                    userRoles,
                    allRoles
                };

                return Ok(roles);
            }
            return NotFound();
        }
    }
}