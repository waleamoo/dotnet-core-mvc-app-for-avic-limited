using AvicLimited.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvicLimited.Data.SeedEntities
{
    public class AppUserSeedConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            var hasher = new PasswordHasher<AppUser>();
            builder.HasData(
                new AppUser
                {
                    Id = "408aa945-3d84-4421-8342-7269ec64d949",
                    Email = "info@aviclimited.com",
                    NormalizedEmail = "INFO@AVICLIMITED.COM",
                    NormalizedUserName = "INFO@AVICLIMITED.COM",
                    UserName = "admin@localhost.com",
                    ClientFirstname = "Admin",
                    ClientLastname = "Admin",
                    ClientCompany = "Avic Limited",
                    ClientAddress = "Unknown",
                    PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                    EmailConfirmed = true
                },
                 new AppUser
                 {
                     Id = "3f4631bd-f907-4409-b416-ba356312e659",
                     Email = "user@aviclimited.com",
                     NormalizedEmail = "USER@AVICLIMITED.COM",
                     NormalizedUserName = "USER@AVICLIMITED.COM",
                     UserName = "user@localhost.com",
                     ClientLastname = "User",
                     ClientFirstname = "User",
                     ClientCompany = "Avic Limited",
                     ClientAddress = "Unknown",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                     EmailConfirmed = true
                 }
                );
        }
    }
}
