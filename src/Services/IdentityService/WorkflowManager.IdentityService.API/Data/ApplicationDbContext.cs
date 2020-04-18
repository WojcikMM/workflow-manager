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

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser()
                {
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Id = Guid.NewGuid().ToString(),
                    PasswordHash = "AQAAAAEAACcQAAAAEMf4Z1kIo2PeUdi8Il3Sap4STZNX8ncdVsTrbRSiras4X0PjVqi67pFMA53OwvpFhw==",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Email = "admin@workflowmanager.com",
                    NormalizedEmail = "ADMIN@WORKFLOWMANAGER.COM",
                    PhoneNumber = "999999999",
                    TwoFactorEnabled = false
                }
            );
        }
    }
}
