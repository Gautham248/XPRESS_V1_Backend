using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int employeeId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(int employeeId, User user);
        Task<bool> DeleteUserAsync(int employeeId);
        Task<IEnumerable<User>> GetSubordinatesAsync(int managerId);
        Task<IEnumerable<User>> GetDepartmentMembersAsync(int duHeadId);
        Task<IEnumerable<TravelRequest>> GetTravelRequestsByUserAsync(int employeeId);
    }
}
