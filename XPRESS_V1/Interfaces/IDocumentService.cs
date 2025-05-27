using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface IDocumentService
    {
        // Get all documents uploaded by an employee
        Task<IEnumerable<Document>> GetDocumentsByEmployeeIdAsync(int employeeId);

        // Get documents by employee and document type (e.g., passport, visa, ID)
        Task<IEnumerable<Document>> GetDocumentsByTypeAsync(int employeeId, int documentTypeId);

        // Get single document by its ID
        Task<Document?> GetDocumentByIdAsync(int documentId);

        // Add new document
        Task<Document> AddDocumentAsync(object dto, int documentTypeId);

        // Update an existing document (if needed)
        Task<Document> UpdateDocumentAsync(int documentId, object dto, int documentTypeId);

        // Optionally: Delete a document
        Task<bool> DeleteDocumentAsync(int documentId);

    }
}
