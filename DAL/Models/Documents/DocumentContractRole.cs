namespace DAL.Models
{
    public class DocumentContractRole : IDocumentRole
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int ContractDocumentId { get; set; }
        public ContractDocument ContractDocument { get; set; }
    }
}