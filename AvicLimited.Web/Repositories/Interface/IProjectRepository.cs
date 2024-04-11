using AvicLimited.Data.Models;
using AvicLimited.Web.Models;

namespace AvicLimited.Web.Repositories.Interface
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<bool> AddProject(ProjectAddVM model);
        Task<List<ProjectListVM>> GetProjects();
        Task<List<ProjectListVM>> GetProjectsByCategory(int? categoryId, int? subCategoryId);
        Task<ProjectListVM> GetProject(int id);
        Task UpdateProject(Project project);
        Task DeleteProject(int id);
        Task ContactAdd(ContactAddVM model);
        Task QuoteAdd(QuoteAddVM model);
    }
}
