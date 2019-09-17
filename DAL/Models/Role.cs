using System.Collections.Generic;

namespace DAL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public List<UserRole> UserRoles { get; set; } // Many-to-many with User
        public List<DocumentCardRoles> DocumentCardRoles { get; set; } // Many-to-many with DocumentCard
    }
}