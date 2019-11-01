using BLL.Repositories.Documents;
using BLL.Interfaces;
using BLL.Interfaces.Documents;
using DAL.DAO.EF;

namespace BLL
{
    public class DataManager : IDataManager
    {
        private ApplicationDbContext _context;
        private IContractDocumentRepository _contractDocuments;

        public DataManager(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Properties
        public IContractDocumentRepository ContractDocuments
        {
            get
            {
                if (_contractDocuments == null)
                    _contractDocuments = new ContractDocumentRepository(_context);
                return _contractDocuments;
            }
        }
        #endregion
    }
}
