using AvicLimited.Data.SeedEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AvicLimited.Data.Models
{
    public class ApplicationContext : IdentityDbContext<AppUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {

        }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SubCategory>()
                .HasOne(c => c.Category)
                .WithMany(w => w.SubCategories)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProjectImage>()
                .HasOne(p => p.Project)
                .WithMany(p => p.ProjectImages)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfiguration(new RolesSeedConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserSeedConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesSeedConfiguration());

            base.OnModelCreating(modelBuilder);

        }
    }
}
