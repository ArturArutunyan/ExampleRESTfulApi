using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TitulWebCards.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public string GetDocuments()
        {
            return "d";
        }

        [HttpGet("{id}", Name = "Get")]
        public string GetDocument(int id)
        {
            return "value";
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
