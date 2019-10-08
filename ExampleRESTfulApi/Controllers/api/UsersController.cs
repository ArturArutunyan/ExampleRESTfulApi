using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExampleRESTfulApi.Controllers.api
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<IdentityUser> GetAll() => _userManager.Users.ToList();

        [HttpGet("{guid}")]
        public async Task<IdentityUser> Get(Guid guid) => await _userManager.FindByIdAsync(guid.ToString());

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] IdentityUser modelUser)
        {
            var user = await _userManager.FindByIdAsync(modelUser.Id);

            if (user == null)
            {
                modelUser.Id = Guid.NewGuid().ToString();
                await _userManager.CreateAsync(modelUser);
                return Created("api/users", modelUser);
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
        public async Task<IActionResult> DeleteUser([FromBody] IdentityUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return Ok();
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("{guid}/password")]
        public async Task<IActionResult> ResetPassword(Guid guid)
        {
            var user = await _userManager.FindByIdAsync(guid.ToString());

            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, resetToken, "111111");
                return Ok();
            }
            return NotFound();
        }
    }
}