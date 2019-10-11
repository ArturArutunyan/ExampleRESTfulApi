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
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<ApplicationUser> GetAll() => _userManager.Users.ToList();

        [HttpGet("{guid}")]
        public async Task<ApplicationUser> Get(Guid guid) => await _userManager.FindByIdAsync(guid.ToString());

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser modelUser)
        {
            var user = await _userManager.FindByIdAsync(modelUser.Id.ToString());

            if (user == null)
            {
                modelUser.Id = Guid.NewGuid();
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
        public async Task<IActionResult> DeleteUser([FromBody] ApplicationUser user)
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