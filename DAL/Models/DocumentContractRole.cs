namespace DAL.Models
{
    public class DocumentContractRole
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int ContractDocumentId { get; set; }
        public ContractDocument ContractDocument { get; set; }
    }
}