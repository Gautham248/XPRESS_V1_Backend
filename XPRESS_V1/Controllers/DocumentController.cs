//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using XPRESS_V1_Backend.Interfaces;
//using XPRESS_V1_Backend.Models;
//using XPRESS_V1_Backend.Models.DTO;
//using XPRESS_V1_Backend.Repositories;

//namespace XPRESS_V1_Backend.Controllers
//{
//    [ApiController]
using Microsoft.AspNetCore.Mvc;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models.DTO;
using XPRESS_V1_Backend.Repositories;

[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _repository;
    private readonly ILogger<DocumentsController> _logger;

    public DocumentsController(IDocumentService repository, ILogger<DocumentsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    // GET: api/documents/employee/5
    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<List<DocumentDto>>> GetAllDocuments(int employeeId)
    {
        try
        {
            var documents = await _repository.GetAllDocumentsByEmployeeAsync(employeeId);
            return Ok(documents);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting documents for employee {EmployeeId}", employeeId);
            return StatusCode(500, "Internal server error");
        }
    }

    // GET: api/documents/employee/5/type/1
    [HttpGet("employee/{employeeId}/type/{idTypeId}")]
    public async Task<ActionResult<List<DocumentDto>>> GetDocumentsByType(int employeeId, int idTypeId)
    {
        try
        {
            if (idTypeId < 1 || idTypeId > 3)
                return BadRequest("Invalid document type ID. Valid values: 1=Passport, 2=Visa, 3=Aadhar");

            var documents = await _repository.GetDocumentsByTypeAsync(employeeId, idTypeId);
            return Ok(documents);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting documents of type {IdTypeId} for employee {EmployeeId}", idTypeId, employeeId);
            return StatusCode(500, "Internal server error");
        }
    }

    // POST: api/documents
    [HttpPost]
    public async Task<IActionResult> CreateDocument([FromBody] DocumentDto documentDto)

    {
        try
        {
            if (documentDto == null)
                return BadRequest("Document can not be null");
            if (documentDto.IDTypeId < 1 || documentDto.IDTypeId > 3)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
                return BadRequest("Invalid document type ID. Valid values: 1=Passport, 2=Visa, 3=Aadhar");

            // Additional validation based on document type
            if (documentDto.IDTypeId == 1 && string.IsNullOrEmpty(documentDto.PassportNumber))
                return BadRequest("Passport number is required");

            var createdDocument = await _repository.AddDocumentAsync(documentDto);
            return CreatedAtAction(
                nameof(GetDocumentsByType),
                new { employeeId = documentDto.EmployeeId, idTypeId = documentDto.IDTypeId },
                createdDocument);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding document");
            return StatusCode(500, "Internal server error");
        }
    }

    // PUT: api/documents
    //[HttpPut]
    //public async Task<ActionResult<DocumentDto>> UpdateDocument([FromBody] DocumentDto documentDto)
    //{
    //    try
    //    {
    //        if (documentDto.IDTypeId < 1 || documentDto.IDTypeId > 3)
    //            return BadRequest("Invalid document type ID. Valid values: 1=Passport, 2=Visa, 3=Aadhar");

    //        if (documentDto.Id == null)
    //            return BadRequest("Document ID is required for update");

    //        var updatedDocument = await _repository.UpdateDocumentAsync(documentDto);
    //        return Ok(updatedDocument);
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Error updating document");
    //        return StatusCode(500, "Internal server error");
    //    }
    //}

    // DELETE: api/documents/5/type/1
    [HttpDelete("{documentId}/type/{idTypeId}")]
    public async Task<IActionResult> DeleteDocument(int documentId, int idTypeId)
    {
        try
        {
            // Add logging to verify the endpoint is hit
            _logger.LogInformation($"Delete request received for ID: {documentId}, Type: {idTypeId}");

            var success = await _repository.DeleteDocumentAsync(documentId, idTypeId);

            if (!success)
            {
                _logger.LogWarning($"Document not found - ID: {documentId}, Type: {idTypeId}");
                return NotFound();
            }

            _logger.LogInformation($"Successfully deleted document - ID: {documentId}, Type: {idTypeId}");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting document - ID: {documentId}, Type: {idTypeId}");
            return StatusCode(500, "Internal server error");
        }
    }
}
//}
