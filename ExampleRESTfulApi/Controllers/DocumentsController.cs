using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DAL.EF;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace TitulWebCards.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private DataManager _dataManager;
        private ApplicationDbContext _db;

        public DocumentsController(DataManager dataManager, ApplicationDbContext db)
        {
            _dataManager = dataManager;
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dataManager.Users.GetAll(true);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IEnumerable<Role>> Get(int id)
        {
            return null;
        }

        [HttpPost]
        public void CreateDocument([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void UpdateDocument(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void DeleteDocument(int id)
        {
        }
    }
}
