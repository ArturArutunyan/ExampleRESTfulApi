using BLL.Interfaces;
using BLL.Interfaces.Documents;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class DataManager
    {
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IContractDocumentRepository ContractDocuments { get; }

        public DataManager(IUserRepository userRepository, IRoleRepository roleRepository, IContractDocumentRepository contractDocumentRepository)
        {
            Users = userRepository;
            Roles = roleRepository;
            ContractDocuments = contractDocumentRepository;
        }
    }
}
