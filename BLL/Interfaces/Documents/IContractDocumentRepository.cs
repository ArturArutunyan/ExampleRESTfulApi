using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces.Documents
{
    public interface IContractDocumentRepository : IRepository<ContractDocument, Guid>
    {
    }
}
