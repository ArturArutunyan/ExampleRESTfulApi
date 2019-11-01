namespace BLL.Entities.Identity
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using DTRole = BLL.Entities.Templates.Identity.Role;
    using DRole = DAL.Entities.Identity.ApplicationRole;

    [Description("Роль")]
    public class Role
    {
        public Role() { }

        public Role(DRole role)
        {
            this.Id = role.Id;
            this.Name = role.Name;
        }

        public Role(DTRole role)
        {
            this.Id = role.Id;
            this.Name = role.Name;
        }

        public Guid Id { get; set; }

        [Key, Description("Наименование")]
        public string Name { get; set; }
    }
}
