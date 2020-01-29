using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorkflowManager.IdentityService.Infrastructure.Context
{
    public class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
