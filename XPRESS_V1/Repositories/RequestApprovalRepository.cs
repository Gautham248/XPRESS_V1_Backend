using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Repositories
{
    public class RequestApprovalRepository : IRequestApprovalService
    {
        private readonly ApiDbContext _context;

        public RequestApprovalRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<RequestApproval> CreateRequestApprovalAsync(RequestApproval requestApproval)
        {
            requestApproval.CreatedAt = new DateTime(2025, 5, 23, 13, 46, 0);
            requestApproval.ActionDate = new DateTime(2025, 5, 23, 13, 46, 0);
            await _context.RequestApprovals.AddAsync(requestApproval);
            await _context.SaveChangesAsync();
            return requestApproval;
        }

        public async Task<RequestApproval> GetRequestApprovalByIdAsync(int approvalId)
        {
            return await _context.RequestApprovals
                .Include(ra => ra.TravelRequest)
                .Include(ra => ra.Approver)
                .Include(ra => ra.PreviousStatus)
                .Include(ra => ra.NewStatus)
                .FirstOrDefaultAsync(ra => ra.ApprovalId == approvalId);
        }

        public async Task<IEnumerable<RequestApproval>> GetAllRequestApprovalsAsync()
        {
            return await _context.RequestApprovals
                .Include(ra => ra.TravelRequest)
                .Include(ra => ra.Approver)
                .Include(ra => ra.PreviousStatus)
                .Include(ra => ra.NewStatus)
                .ToListAsync();
        }

        public async Task<RequestApproval> UpdateRequestApprovalAsync(int approvalId, RequestApproval requestApproval)
        {
            var existingApproval = await _context.RequestApprovals.FindAsync(approvalId);
            if (existingApproval == null)
                return null;

            existingApproval.RequestId = requestApproval.RequestId;
            existingApproval.ApproverId = requestApproval.ApproverId;
            existingApproval.ApprovalLevel = requestApproval.ApprovalLevel;
            existingApproval.ActionTaken = requestApproval.ActionTaken;
            existingApproval.Comments = requestApproval.Comments;
            existingApproval.PreviousStatusId = requestApproval.PreviousStatusId;
            existingApproval.NewStatusId = requestApproval.NewStatusId;
            existingApproval.ActionDate = new DateTime(2025, 5, 23, 13, 46, 0);

            await _context.SaveChangesAsync();
            return existingApproval;
        }

        public async Task<bool> DeleteRequestApprovalAsync(int approvalId)
        {
            var approval = await _context.RequestApprovals.FindAsync(approvalId);
            if (approval == null)
                return false;

            _context.RequestApprovals.Remove(approval);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RequestApproval>> GetApprovalsByRequestAsync(int requestId)
        {
            return await _context.RequestApprovals
                .Where(ra => ra.RequestId == requestId)
                .Include(ra => ra.Approver)
                .Include(ra => ra.PreviousStatus)
                .Include(ra => ra.NewStatus)
                .ToListAsync();
        }

        public async Task<IEnumerable<RequestApproval>> GetApprovalsByApproverAsync(int approverId)
        {
            return await _context.RequestApprovals
                .Where(ra => ra.ApproverId == approverId)
                .Include(ra => ra.TravelRequest)
                .Include(ra => ra.PreviousStatus)
                .Include(ra => ra.NewStatus)
                .ToListAsync();
        }
    }
}
