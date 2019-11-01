namespace BLL.Entities.Identity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DTUser = BLL.Entities.Templates.Identity.User;
    using DUser = DAL.Entities.Identity.ApplicationUser;

    [Description("Учетная запись")]
    public class User
    {
        public User() { }

        public User(DUser user)
        {
            this.Id = user.Id;
            this.Login = user.UserName;
            this.Email = user.Email;
        }

        public User(DTUser user)
        {
            this.Id = user.Id;
            this.Login = user.Login;
            this.Email = user.Email;
            this.Roles = user.Roles.Select(x => new Role(x)).ToArray();
        }

        public Guid Id { get; set; }

        [Key, Description("Логин")]
        public string Login { get; set; }

        [Description("Email")]
        public string Email { get; set; }

        public IEnumerable<Role> Roles { get; set; }
    }
}
