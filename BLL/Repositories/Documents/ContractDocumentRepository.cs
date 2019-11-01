using BLL.Interfaces.Documents;
using DAL.DAO.EF;
using DAL.Entities.Documents;
using System;

namespace BLL.Repositories.Documents
{
    public class ContractDocumentRepository : Repository<ContractDocument, Guid>, IContractDocumentRepository
    {
        public ContractDocumentRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
