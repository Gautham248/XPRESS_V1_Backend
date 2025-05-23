using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Repositories
{
    public class UserRepository : IUserService
    {
        private readonly ApiDbContext _context;

        public UserRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.CreatedAt = new DateTime(2025, 5, 23, 13, 46, 0); // Current time: 01:46 PM IST
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int employeeId)
        {
            return await _context.Users
                .Include(u => u.ReportingManager)
                .Include(u => u.DuHead)
                .FirstOrDefaultAsync(u => u.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.ReportingManager)
                .Include(u => u.DuHead)
                .ToListAsync();
        }

        public async Task<User> UpdateUserAsync(int employeeId, User user)
        {
            var existingUser = await _context.Users.FindAsync(employeeId);
            if (existingUser == null)
                return null;

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Role = user.Role;
            existingUser.Department = user.Department;
            existingUser.IsActive = user.IsActive;
            existingUser.ReportingManagerId = user.ReportingManagerId;
            existingUser.DuHeadId = user.DuHeadId;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int employeeId)
        {
            var user = await _context.Users.FindAsync(employeeId);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetSubordinatesAsync(int managerId)
        {
            return await _context.Users
                .Where(u => u.ReportingManagerId == managerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetDepartmentMembersAsync(int duHeadId)
        {
            return await _context.Users
                .Where(u => u.DuHeadId == duHeadId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TravelRequest>> GetTravelRequestsByUserAsync(int employeeId)
        {
            return await _context.TravelRequests
                .Where(tr => tr.EmployeeId == employeeId)
                .Include(tr => tr.TravelType)
                .Include(tr => tr.TripType)
                .Include(tr => tr.Project)
                .Include(tr => tr.TravelMode)
                .Include(tr => tr.CurrentStatus)
                .ToListAsync();
        }
    }
}
