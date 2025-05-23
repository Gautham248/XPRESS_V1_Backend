using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Repositories
{
    public class AuditLogRepository : IAuditLogService
    {
        private readonly ApiDbContext _context;

        public AuditLogRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<AuditLog> CreateAuditLogAsync(AuditLog auditLog)
        {
            auditLog.Timestamp = DateTime.UtcNow;
            auditLog.Comments ??= string.Empty;

            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();
            return auditLog;
        }

        public async Task<AuditLog> GetAuditLogByIdAsync(int logId)
        {
            return await _context.AuditLogs
                .Include(al => al.TravelRequest)
                .Include(al => al.User)
                .Include(al => al.OldStatus)
                .Include(al => al.NewStatus)
                .FirstOrDefaultAsync(al => al.LogId == logId);
        }

        public async Task<IEnumerable<AuditLog>> GetAllAuditLogsAsync()
        {
            return await _context.AuditLogs
                .Include(al => al.TravelRequest)
                .Include(al => al.User)
                .Include(al => al.OldStatus)
                .Include(al => al.NewStatus)
                .ToListAsync();
        }

        public async Task<AuditLog> UpdateAuditLogAsync(int logId, AuditLog auditLog)
        {
            var existingLog = await _context.AuditLogs.FindAsync(logId);
            if (existingLog == null)
                return null;

            existingLog.RequestId = auditLog.RequestId;
            existingLog.UserId = auditLog.UserId;
            existingLog.ActionType = auditLog.ActionType;
            existingLog.OldStatusId = auditLog.OldStatusId;
            existingLog.NewStatusId = auditLog.NewStatusId;
            existingLog.ChangeDescription = auditLog.ChangeDescription;
            existingLog.Comments = auditLog.Comments ?? string.Empty;
            existingLog.IpAddress = auditLog.IpAddress;
            existingLog.Timestamp = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingLog;
        }

        public async Task<bool> DeleteAuditLogAsync(int logId)
        {
            var auditLog = await _context.AuditLogs.FindAsync(logId);
            if (auditLog == null)
                return false;

            _context.AuditLogs.Remove(auditLog);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsByRequestAsync(int requestId)
        {
            return await _context.AuditLogs
                .Where(al => al.RequestId == requestId)
                .Include(al => al.User)
                .Include(al => al.OldStatus)
                .Include(al => al.NewStatus)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAuditLogsByUserAsync(int userId)
        {
            return await _context.AuditLogs
                .Where(al => al.UserId == userId)
                .Include(al => al.TravelRequest)
                .Include(al => al.OldStatus)
                .Include(al => al.NewStatus)
                .ToListAsync();
        }
    }
}
