using BLL.Interfaces.Documents;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDataManager
    {
        IContractDocumentRepository ContractDocuments { get; }
    }
}
