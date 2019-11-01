using DAL.Entities.Identity;
using System;

namespace BLL.Entities.Templates.Identity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public Role[] Roles { get; set; }

        public static implicit operator ApplicationUser(User user)
        {
            return new ApplicationUser()
            {
                Id = user.Id,
                UserName = user.Login,
                Email = user.Email
            };
        }

        public static explicit operator User(ApplicationUser user)
        {
            return new User()
            {
                Id = user.Id,
                Login = user.UserName,
                Email = user.Email
            };
        }
    }
}
