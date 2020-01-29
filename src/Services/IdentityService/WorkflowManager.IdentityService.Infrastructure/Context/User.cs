using System;
using System.Collections.Generic;

namespace WorkflowManager.IdentityService.Infrastructure.Context
{
    public class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
