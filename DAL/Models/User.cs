using System.Collections.Generic;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
