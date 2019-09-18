using System.Collections.Generic;

namespace DAL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<DocumentContractRole> DocumentContractRoles { get; set; }
    }
}