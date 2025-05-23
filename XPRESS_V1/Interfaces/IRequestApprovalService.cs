using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface IRequestApprovalService
    {
        Task<RequestApproval> CreateRequestApprovalAsync(RequestApproval requestApproval);
        Task<RequestApproval> GetRequestApprovalByIdAsync(int approvalId);
        Task<IEnumerable<RequestApproval>> GetAllRequestApprovalsAsync();
        Task<RequestApproval> UpdateRequestApprovalAsync(int approvalId, RequestApproval requestApproval);
        Task<bool> DeleteRequestApprovalAsync(int approvalId);
        Task<IEnumerable<RequestApproval>> GetApprovalsByRequestAsync(int requestId);
        Task<IEnumerable<RequestApproval>> GetApprovalsByApproverAsync(int approverId);
    }
}
