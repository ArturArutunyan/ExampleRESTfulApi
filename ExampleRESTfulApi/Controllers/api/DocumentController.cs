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
        public async Task<IEnumerable<ContractDocument>> Get() => await _dataManager.ContractDocuments.GetAll();
        [HttpGet("{guid}")]
        public async Task<IActionResult> Get(Guid guid)
        {
            var document = await _dataManager.ContractDocuments.GetWhere(c => c.Guid == guid);

            if (document != null)
                return Ok(document);
            return NotFound();
        }

        /// <remarks>
        /// <b>Данные в примере являются валидными</b>
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContractDocument contractDocument)
        {
            var document = await _dataManager.ContractDocuments.GetWhere(c => c.Guid == contractDocument.Guid);

            if (document == null)
            {
                await _dataManager.ContractDocuments.Create(contractDocument);
                document = await _dataManager.ContractDocuments.GetWhere(c => c.Title == contractDocument.Title); // возвращаем новый обьект со сгенерированным Guid
                return Created("api/documents/", document);                                                    
            }
            return Conflict();

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContractDocument contractDocument)
        {
            var document = await _dataManager.ContractDocuments.GetWhere(c => c.Guid == contractDocument.Guid);

            if (document != null)
            {
                await _dataManager.ContractDocuments.Update(contractDocument);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var document = await _dataManager.ContractDocuments.GetWhere(c => c.Guid == guid);

            if (document != null)
            {
                await _dataManager.ContractDocuments.Delete(document);
                return Ok();
            }
            return NotFound();
        }
    }
}