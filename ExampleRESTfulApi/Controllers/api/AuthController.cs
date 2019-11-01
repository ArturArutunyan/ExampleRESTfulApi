using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Identity;
using JwtAuthentication.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthentication.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        
        [Route("register")]
        [HttpPost]   
        public async Task<IActionResult> InsertUser([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Login,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                return Ok(model);
            }
            return Conflict();
            
        }

        /// <remarks>
        /// <b>Данные в примере являются валидными</b>
        /// </remarks>
        [Route("login")]   
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var identity = await GetIdentity(user);

                var signinKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Site"],
                  audience: _configuration["Jwt:Site"],
                  expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                  claims: identity.Claims,
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(
                  new
                  {
                      token = new JwtSecurityTokenHandler().WriteToken(token),
                      expiration = token.ValidTo
                  });
            }
            return Unauthorized();
        }


        // Возвращает обьект ClaimsIdentity в котором находиться список Claims с ролями
        private async Task<ClaimsIdentity> GetIdentity(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user); // Берем все роли данного пользователя
            var claims = new List<Claim>();

            // Помещаем их в список claims, это нужно для авторизации по ролям
            foreach (var role in roles)
            {
               claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role)); 
            }     

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
        }
    }

}