using System.Collections.Generic;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
