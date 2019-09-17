namespace DAL.Models
{
    public class DocumentCardRoles
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int DocumentCardId { get; set; }
        public DocumentCard DocumentCard { get; set; }
    }
}