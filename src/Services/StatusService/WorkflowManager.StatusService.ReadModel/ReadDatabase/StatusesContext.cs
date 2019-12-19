﻿using Microsoft.EntityFrameworkCore;

namespace WorkflowManager.StatusService.ReadModel.ReadDatabase
{
    public class StatusesContext : DbContext
    {
        public StatusesContext(DbContextOptions<StatusesContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<StatusModel> Statuses { get; set; }
    }
}
