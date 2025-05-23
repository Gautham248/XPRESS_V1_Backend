using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface IAuditLogService
    {
        Task<AuditLog> CreateAuditLogAsync(AuditLog auditLog);
        Task<AuditLog> GetAuditLogByIdAsync(int logId);
        Task<IEnumerable<AuditLog>> GetAllAuditLogsAsync();
        Task<AuditLog> UpdateAuditLogAsync(int logId, AuditLog auditLog);
        Task<bool> DeleteAuditLogAsync(int logId);
        Task<IEnumerable<AuditLog>> GetAuditLogsByRequestAsync(int requestId);
        Task<IEnumerable<AuditLog>> GetAuditLogsByUserAsync(int userId);
    }
}
