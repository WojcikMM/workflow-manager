using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityServerAspNetIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001").ToString(),
                    Name = "processes_manager",
                    NormalizedName = "PROCESSES_MANAGER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                  new IdentityRole()
                  {
                      Id = new Guid("00000000-0000-0000-0000-000000000002").ToString(),
                      Name = "statuses_manager",
                      NormalizedName = "STATUSES_MANAGER",
                      ConcurrencyStamp = Guid.NewGuid().ToString()
                  }
                );

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser()
                {
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Id = new Guid("0f732d1f-def8-4d72-82a3-d5609fd4ea5e").ToString(),
                    PasswordHash = "AQAAAAEAACcQAAAAEMf4Z1kIo2PeUdi8Il3Sap4STZNX8ncdVsTrbRSiras4X0PjVqi67pFMA53OwvpFhw==",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Email = "admin@workflowmanager.com",
                    NormalizedEmail = "ADMIN@WORKFLOWMANAGER.COM",
                    PhoneNumber = "999999999",
                    TwoFactorEnabled = false
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = new Guid("00000000-0000-0000-0000-000000000001").ToString(),
                    UserId = new Guid("0f732d1f-def8-4d72-82a3-d5609fd4ea5e").ToString()
                },
                new IdentityUserRole<string>()
                {
                    RoleId = new Guid("00000000-0000-0000-0000-000000000002").ToString(),
                    UserId = new Guid("0f732d1f-def8-4d72-82a3-d5609fd4ea5e").ToString()
                }
                );
        }
    }
}
