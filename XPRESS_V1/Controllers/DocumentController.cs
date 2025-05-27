using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;
using XPRESS_V1_Backend.Models.DTO;
using XPRESS_V1_Backend.Repositories;

namespace XPRESS_V1_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var result = await _documentService.GetDocumentByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        //[HttpPost("document/create")]
        //public async Task<IActionResult> Create([FromBody] object dto, [FromQuery] int documentTypeId)
        //{
        //    var result = await _documentService.AddDocumentAsync(dto, documentTypeId);
        //    return result == null ? BadRequest("Invalid data or document expired.") : Ok(result);
        //}

        [HttpPost("/document/add")]
        public async Task<IActionResult> AddDocument([FromBody] object dto, [FromQuery] int documentTypeId)
        {
            var result = await _documentService.AddDocumentAsync(dto, documentTypeId);
            return result == null ? BadRequest("Invalid data or document expired.") : Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] object dto, [FromQuery] int documentTypeId)
        {
            var result = await _documentService.UpdateDocumentAsync(id, dto, documentTypeId);
            return result == null ? NotFound() : Ok(result);
        }


    }
}
