using System;
using System.Threading.Tasks;
using BLL.Entities.Templates.Identity;
using BLL.Managers.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExampleRESTfulApi.Controllers.api
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Users Users;
        public UsersController(Users users)
        {
            Users = users;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(Users.GetAllAsync());

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {
            var user = await Users.GetAsyncById(guid);

            if (user != null)
                return Ok(user);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var resultCreated = await Users.CreateAsync(user);

            if (resultCreated)
                return Created("api/users", user);
            return Conflict(); 
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User model)
        {
            var user = await Users.UpdateAsync(model);
            
            if (user)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid guid)
        {
            var result = await Users.DeleteAsync(guid);

            if (result)
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
            var user = await Users.ResetPasswordAsync(guid);

            if (user != null)
                return Ok(user);
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
            var roles = await Users.GetAllUserRolesAsync(guid);

            if (roles != null)
            {
                return Ok(new { roles });
            }
            return NotFound();
        }
    }
}