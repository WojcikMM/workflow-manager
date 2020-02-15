using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityServerAspNetIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole("Processes Manager"),
                new IdentityRole("Statuses Manager")
                );

            var user = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "sample@email.pl",
                UserName = "user1",
                NormalizedUserName = "sample_user"              
            };

            user.PasswordHash =  new PasswordHasher<IdentityUser>().HashPassword(user, "passwd1");


            builder.Entity<IdentityUser>().HasData(
                user
              );

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<WorkflowManager.IdentityService.API.Quickstart.Account.RegistationViewModel> RegistationViewModel { get; set; }
    }
}
