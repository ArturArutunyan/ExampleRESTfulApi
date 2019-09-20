using BLL.Interfaces;
using BLL.Interfaces.Documents;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class DataManger
    {
        private IUserRepository _userRepository { get; }
        private IRoleRepository _roleRepository { get; }
        private IContractDocumentRepository _contractDocumentRepository { get; }

        public DataManger(IUserRepository userRepository, IRoleRepository roleRepository, IContractDocumentRepository contractDocumentRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _contractDocumentRepository = contractDocumentRepository;
        }
    }
}
