using BLL.Interfaces.Documents;

namespace BLL.Interfaces
{
    public interface IDataManager
    {
        IContractDocumentRepository ContractDocuments { get; }
    }
}
