using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Models;
using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models.DTO;

namespace XPRESS_V1_Backend.Repositories
{
    public class DocumentRepository : IDocumentService
    {
        private readonly ApiDbContext _context;

        public DocumentRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetDocumentsByEmployeeIdAsync(int employeeId)
        {
            return await _context.Documents
                .Include(d => d.DocumentType)
                .Include(d => d.Employee)
                .Where(d => d.EmployeeID == employeeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetDocumentsByTypeAsync(int employeeId, int documentTypeId)
        {
            return await _context.Documents
                .Include(d => d.DocumentType)
                .Include(d => d.Employee)
                .Where(d => d.EmployeeID == employeeId && d.DocumentTypeID == documentTypeId)
                .ToListAsync();
        }

        public async Task<Document?> GetDocumentByIdAsync(int documentId)
        {
            return await _context.Documents
                .Include(d => d.DocumentType)
                //.Include(d => d.Employee)
                .FirstOrDefaultAsync(d => d.DocumentID == documentId);
        }

        public async Task<Document> AddDocumentAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<Document> UpdateDocumentAsync(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<bool> DeleteDocumentAsync(int documentId)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null)
                return false;

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return true;
        }

        // AddDocument with DTO
        public async Task<Document?> AddDocumentAsync(object dto, int documentTypeId)
        {
            Document? document = null;

            switch (dto)
            {
                case VisaDTO visa when visa.ExpiryDate > DateTime.Today:
                    document = new Document
                    {
                        DocumentNumber = visa.DocumentNumber,
                        VisaClass = visa.VisaClass,
                        IssuingCountry = visa.IssuingCountry,
                        IssuingPost = visa.IssuingPost,
                        IssueDate = visa.IssueDate,
                        ExpiryDate = visa.ExpiryDate,
                        DocumentTypeID = documentTypeId,
                        EmployeeID = visa.EmployeeID
                    };
                    break;

                case PassportDTO passport when passport.ExpiryDate > DateTime.Today:
                    document = new Document
                    {
                        DocumentNumber = passport.DocumentNumber,
                        IssuingCountry = passport.IssuingCountry,
                        IssueDate = passport.IssueDate,
                        ExpiryDate = passport.ExpiryDate,
                        DocumentTypeID = documentTypeId,
                        EmployeeID = passport.EmployeeID
                    };
                    break;

                case VoterIDDTO voter:
                    document = new Document
                    {
                        DocumentNumber = voter.DocumentNumber,
                        IssuingCountry = voter.IssuingCountry,
                        IssueDate = voter.IssueDate,
                        ExpiryDate = voter.ExpiryDate,
                        DocumentTypeID = documentTypeId,
                        EmployeeID = voter.EmployeeID
                    };
                    break;

                case AadharDTO aadhar when !string.IsNullOrEmpty(aadhar.DocumentNumber):
                    document = new Document
                    {
                        DocumentNumber = aadhar.DocumentNumber,
                        IssuingCountry = aadhar.IssuingCountry,
                        IssueDate = aadhar.IssueDate,
                        ExpiryDate = aadhar.ExpiryDate,
                        DocumentTypeID = documentTypeId,
                        EmployeeID = aadhar.EmployeeID
                    };
                    break;

                case DrivingLicenceDTO dl when dl.ExpiryDate > DateTime.Today:
                    document = new Document
                    {
                        DocumentNumber = dl.DocumentNumber,
                        IssuingCountry = dl.IssuingCountry,
                        IssueDate = dl.IssueDate,
                        ExpiryDate = dl.ExpiryDate,
                        DocumentTypeID = documentTypeId,
                        EmployeeID = dl.EmployeeID
                    };
                    break;

                default:
                    return null;
            }

            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
            return document;
        }


        // UpdateDocument with DTO
        public async Task<Document?> UpdateDocumentAsync(int documentId, object dto, int documentTypeId)
        {
            var existing = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentID == documentId);
            if (existing == null) return null;

            switch (dto)
            {
                case VisaDTO visa when visa.ExpiryDate > DateTime.Today:
                    existing.DocumentNumber = visa.DocumentNumber;
                    existing.VisaClass = visa.VisaClass;
                    existing.IssuingCountry = visa.IssuingCountry;
                    existing.IssuingPost = visa.IssuingPost;
                    existing.IssueDate = visa.IssueDate;
                    existing.ExpiryDate = visa.ExpiryDate;
                    existing.EmployeeID = visa.EmployeeID;
                    break;

                case PassportDTO passport when passport.ExpiryDate > DateTime.Today:
                    existing.DocumentNumber = passport.DocumentNumber;
                    existing.IssuingCountry = passport.IssuingCountry;
                    existing.IssueDate = passport.IssueDate;
                    existing.ExpiryDate = passport.ExpiryDate;
                    existing.EmployeeID = passport.EmployeeID;
                    break;

                case VoterIDDTO voter:
                    existing.DocumentNumber = voter.DocumentNumber;
                    existing.IssuingCountry = voter.IssuingCountry;
                    existing.IssueDate = voter.IssueDate;
                    existing.ExpiryDate = voter.ExpiryDate;
                    existing.EmployeeID = voter.EmployeeID;
                    break;

                case AadharDTO aadhar when !string.IsNullOrEmpty(aadhar.DocumentNumber):
                    existing.DocumentNumber = aadhar.DocumentNumber;
                    existing.IssuingCountry = aadhar.IssuingCountry;
                    existing.IssueDate = aadhar.IssueDate;
                    existing.ExpiryDate = aadhar.ExpiryDate;
                    existing.EmployeeID = aadhar.EmployeeID;
                    break;

                case DrivingLicenceDTO dl when dl.ExpiryDate > DateTime.Today:
                    existing.DocumentNumber = dl.DocumentNumber;
                    existing.IssuingCountry = dl.IssuingCountry;
                    existing.IssueDate = dl.IssueDate;
                    existing.ExpiryDate = dl.ExpiryDate;
                    existing.EmployeeID = dl.EmployeeID;
                    break;

                default:
                    return null;
            }

            _context.Documents.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

    }
}
