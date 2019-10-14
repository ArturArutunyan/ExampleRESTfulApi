using BLL.Interfaces.Documents;
using DAL.EF;
using DAL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Implementation.Repositories.Documents
{
    public class ContractDocumentRepository : Repository<ContractDocument, Guid>, IContractDocumentRepository
    {
        public ContractDocumentRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
