using AvicLimited.Data.Models;
using AvicLimited.Web.Models;
using AvicLimited.Web.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AvicLimited.Web.Repositories.Implementation
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly ApplicationContext _context;

        public ProjectRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddProject(ProjectAddVM model)
        {
            Project project = new Project
            { 
                ProjectName = model.ProjectName,
                ProjectDescription = model.ProjectDescription,
                CategoryId = model.CategoryId,
                SubCategoryId = model.SubCategoryId ?? null,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };
            project.ProjectImages = new List<ProjectImage>();
            foreach (var file in model.Gallery)
            {
                project.ProjectImages.Add(new ProjectImage
                {
                    ProjectImageDescription = file.Name,
                    ProjectImageUrl = file.URL,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow
                });
            }

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProjectListVM>> GetProjects()
        {
            var projects = await _context.Projects
                .Include(x => x.ProjectImages)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .ToListAsync();
            if (projects is not null)
            {
                return projects.Select(x => new ProjectListVM
                {
                    Id = x.Id,
                    ProjectName = x.ProjectName,
                    ProjectDescription = x.ProjectDescription,
                    CategoryName = x.Category.CategoryName,
                    SubcategoryName = x.SubCategory != null ? x.SubCategory.SubCategoryName : "",
                    ProjectImages = x.ProjectImages.Select(c => new ProjectImage
                    {
                        Id = c.Id,
                        ProjectImageDescription = c.ProjectImageDescription,
                        ProjectImageUrl = c.ProjectImageUrl
                    }).ToList()
                }).ToList();
            }
            return new List<ProjectListVM>();
        }

        public async Task<List<ProjectListVM>> GetProjectsByCategory(int? categoryId, int? subCategoryId)
        {
            if (categoryId > 0)
            {
                var projectsByCategory = await _context.Projects.Include(x => x.ProjectImages).Where(x => x.CategoryId == categoryId).ToListAsync();
                return projectsByCategory.Select(x => new ProjectListVM
                {
                    Id = x.Id,
                    ProjectName = x.ProjectName,
                    ProjectDescription = x.ProjectDescription,
                    CategoryName = _context.Categories.SingleOrDefault(u => u.Id == x.CategoryId).CategoryName,
                    SubcategoryName = "",
                    ProjectImages = x.ProjectImages.Select(c => new ProjectImage
                    {
                        Id = c.Id,
                        ProjectImageDescription = c.ProjectImageDescription,
                        ProjectImageUrl = c.ProjectImageUrl
                    }).ToList()
                }).ToList();
            }

            var projectsBySubcategory = await _context.Projects.Include(x => x.ProjectImages).Where(x => x.SubCategoryId == subCategoryId).ToListAsync();
            return projectsBySubcategory.Select(x => new ProjectListVM
            {
                Id = x.Id,
                ProjectName = x.ProjectName,
                ProjectDescription = x.ProjectDescription,
                CategoryName = _context.Categories.SingleOrDefault(u => u.Id == x.CategoryId).CategoryName,
                SubcategoryName = _context.SubCategories.SingleOrDefault(u => u.Id == x.SubCategoryId).SubCategoryName ?? "",
                ProjectImages = x.ProjectImages.Select(c => new ProjectImage
                {
                    Id = c.Id,
                    ProjectImageDescription = c.ProjectImageDescription,
                    ProjectImageUrl = c.ProjectImageUrl
                }).ToList()
            }).ToList();

        }

        public async Task<ProjectListVM> GetProject(int id)
        {
            return await _context.Projects.Include(x => x.ProjectImages)
                .Where(x => x.Id == id)
                .Select(x => new ProjectListVM
                {
                    Id = x.Id,
                    ProjectName = x.ProjectName,
                    ProjectDescription = x.ProjectDescription,
                    CategoryName = _context.Categories.SingleOrDefault(u => u.Id == x.CategoryId).CategoryName,
                    SubcategoryName = _context.SubCategories.SingleOrDefault(u => u.Id == x.SubCategoryId).SubCategoryName ?? "",
                    ProjectImages = x.ProjectImages.Select(c => new ProjectImage
                    {
                        Id = c.Id,
                        ProjectImageDescription = c.ProjectImageDescription,
                        ProjectImageUrl = c.ProjectImageUrl
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task DeleteProject(int id)
        {
            var project = await _context.Projects
                .Include(x => x.ProjectImages)
                .Where(x => x.Id == id).FirstAsync();
            if (project is not null)
            {
                await DeleteAsync(id);
            }
        }

        public async Task UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        // Non-project-specific method 
        public async Task ContactAdd(ContactAddVM model)
        {
            Contact contact = new Contact()
            {
                ContactEmail = model.ContactEmail,
                ContactName = model.ContactName,
                ContactMessage = model.ContactMessage,
                ContactSubject = model.ContactSubject,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task QuoteAdd(QuoteAddVM model)
        {
            Quote quote = new Quote()
            {
                QuoteBudget = model.QuoteBudget,
                QuoteEmail = model.QuoteEmail,
                QuoteMessage = model.QuoteMessage,
                QuoteName = model.QuoteName,
                QuotePhone = model.QuotePhone,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();
        }
    }
}
