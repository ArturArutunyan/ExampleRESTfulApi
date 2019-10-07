using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExampleRESTfulApi.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDataManager _dataManager;
        public DocumentController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [HttpGet]
        public async Task<IEnumerable<ContractDocument>> Get()
        {
            return await _dataManager.ContractDocuments.GetAll();
        }

        [HttpGet("{guid}")]
        public async Task<ContractDocument> Get(Guid guid)
        {
            return await _dataManager.ContractDocuments.GetWhere(guid);
        }

        /// <remarks>
        /// <b>Данные в примере являются валидными</b>
        /// </remarks>
        [HttpPost]
        public async Task Create([FromBody]ContractDocument contractDocument)
        {
            await _dataManager.ContractDocuments.Create(contractDocument);
        }

        [HttpPut]
        public async Task Update([FromBody]ContractDocument contractDocument)
        {
            await _dataManager.ContractDocuments.Update(contractDocument);
        }

        [HttpDelete("{guid}")]
        public async Task Delete(Guid guid)
        {
            await _dataManager.ContractDocuments.Delete(guid);
        }
    }
}