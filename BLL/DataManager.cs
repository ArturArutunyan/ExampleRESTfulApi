using BLL.Interfaces.Documents;

namespace BLL
{
    public class DataManager
    {
        public IContractDocumentRepository ContractDocuments { get; }

        public DataManager(IContractDocumentRepository contractDocumentRepository)
        {
            ContractDocuments = contractDocumentRepository;
        }
    }
}
