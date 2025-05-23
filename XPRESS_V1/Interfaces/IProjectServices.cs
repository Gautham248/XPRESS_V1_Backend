using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProjectAsync(Project project);
        Task<Project> GetProjectByCodeAsync(string projectCode);
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> UpdateProjectAsync(string projectCode, Project project);
        Task<bool> DeleteProjectAsync(string projectCode);
        Task<IEnumerable<TravelRequest>> GetTravelRequestsByProjectAsync(string projectCode);
    }
}
