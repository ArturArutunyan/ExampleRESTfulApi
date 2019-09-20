namespace DAL.Models
{
    public interface IDocumentRole
    {
        int RoleId { get; set; }
        Role Role { get; set; }
    }
}