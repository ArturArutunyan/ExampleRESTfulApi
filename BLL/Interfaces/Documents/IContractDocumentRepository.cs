﻿using DAL.Entities.Documents;
using System;

namespace BLL.Interfaces.Documents
{
    public interface IContractDocumentRepository : IRepository<ContractDocument, Guid>
    {
    }
}
