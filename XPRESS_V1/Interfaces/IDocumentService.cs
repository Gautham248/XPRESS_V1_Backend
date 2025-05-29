using XPRESS_V1_Backend.Models;
using XPRESS_V1_Backend.Models.DTO;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface IDocumentService
    {
        // Document operations
        Task<DocumentDto> AddDocumentAsync(DocumentDto documentDto);
        Task<DocumentDto> UpdateDocumentAsync(DocumentDto documentDto);
        Task<bool> DeleteDocumentAsync(int documentId, int idTypeId);

        // Get operations
        Task<List<DocumentDto>> GetAllDocumentsByEmployeeAsync(int employeeId);
        Task<List<DocumentDto>> GetDocumentsByTypeAsync(int employeeId, int idTypeId);
        Task<DocumentDto> GetDocumentByIdAsync(int documentId, int idTypeId);
    }
}
