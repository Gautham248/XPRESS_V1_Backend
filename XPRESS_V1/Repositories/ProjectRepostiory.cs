using Microsoft.EntityFrameworkCore;
using XPRESS_V1_Backend.Data;
using XPRESS_V1_Backend.Interfaces;
using XPRESS_V1_Backend.Models;

namespace XPRESS_V1_Backend.Repositories
{
    public class ProjectRepository : IProjectService
    {
        private readonly ApiDbContext _context;

        public ProjectRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            project.CreatedAt = new DateTime(2025, 5, 23, 13, 46, 0);
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> GetProjectByCodeAsync(string projectCode)
        {
            return await _context.Projects.FindAsync(projectCode);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> UpdateProjectAsync(string projectCode, Project project)
        {
            var existingProject = await _context.Projects.FindAsync(projectCode);
            if (existingProject == null)
                return null;

            existingProject.ProjectName = project.ProjectName;
            existingProject.ProjectDescription = project.ProjectDescription;
            existingProject.ProjectManager = project.ProjectManager;
            existingProject.IsActive = project.IsActive;

            await _context.SaveChangesAsync();
            return existingProject;
        }

        public async Task<bool> DeleteProjectAsync(string projectCode)
        {
            var project = await _context.Projects.FindAsync(projectCode);
            if (project == null)
                return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TravelRequest>> GetTravelRequestsByProjectAsync(string projectCode)
        {
            return await _context.TravelRequests
                .Where(tr => tr.ProjectCode == projectCode)
                .Include(tr => tr.Employee)
                .Include(tr => tr.TravelType)
                .Include(tr => tr.TripType)
                .Include(tr => tr.Project)
                .Include(tr => tr.TravelMode)
                .Include(tr => tr.CurrentStatus)
                .ToListAsync();
        }
    }
}
