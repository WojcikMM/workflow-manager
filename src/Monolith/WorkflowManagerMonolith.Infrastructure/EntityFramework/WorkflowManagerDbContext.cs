using Microsoft.EntityFrameworkCore;
using System;
using WorkflowManagerMonolith.Infrastructure.EntityFramework.Models;

namespace WorkflowManagerMonolith.Infrastructure.EntityFramework
{
    public class WorkflowManagerDbContext : DbContext
    {
        public WorkflowManagerDbContext(DbContextOptions<WorkflowManagerDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<ApplicationModel> Applications { get; set; }
        public DbSet<TransactionItemModel> TransactionItems { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primmary Keys
            modelBuilder.Entity<StatusModel>().HasKey(status => status.Id);
            modelBuilder.Entity<TransactionModel>().HasKey(transaction => transaction.Id);
            modelBuilder.Entity<UserModel>().HasKey(user => user.Id);
            modelBuilder.Entity<ApplicationModel>().HasKey(applicaiton => applicaiton.Id);

            // Relations

            // -- Status Model

            modelBuilder.Entity<StatusModel>()
                .HasMany(status => status.AvailableTransactions)
                .WithOne(transaction => transaction.IncomingStatus)
                .HasForeignKey(transaction => transaction.IncomingStatusId)
                .IsRequired();

            modelBuilder.Entity<StatusModel>()
                .HasMany(status => status.IncomingTransactions)
                .WithOne(transaction => transaction.OutgoingStatus)
                .HasForeignKey(x => x.OutgoingStatusId)
                .IsRequired();

            modelBuilder.Entity<StatusModel>()
                .HasMany(status => status.ApplicationsWithStatus)
                .WithOne(application => application.Status)
                .HasForeignKey(application => application.StatusId)
                .IsRequired(false);


            // -- Transaction Item Model
            modelBuilder.Entity<TransactionItemModel>()
                .HasKey(transactionItem => transactionItem.Id);

            modelBuilder.Entity<TransactionItemModel>()
                .HasOne(transactionItem => transactionItem.Application)
                .WithMany(application => application.TransactionItems)
                .HasForeignKey(transactionItem => transactionItem.ApplicationId)
                .IsRequired();

            modelBuilder.Entity<TransactionItemModel>()
                .HasOne(transactionItem => transactionItem.Transaction)
                .WithMany(transaction => transaction.TransactionItems)
                .HasForeignKey(transactionItem => transactionItem.TransactionId)
                .IsRequired();

            modelBuilder.Entity<TransactionItemModel>()
                .HasOne(transactionItem => transactionItem.User)
                .WithMany(user => user.TransactionItems)
                .HasForeignKey(transactionitems => transactionitems.UserId)
                .IsRequired();

            // -- User Model
            modelBuilder.Entity<UserModel>()
                .HasMany(user => user.AssignedApplications)
                .WithOne(application => application.User)
                .HasForeignKey(application => application.UserId);


            // Seed Data

            modelBuilder.Entity<StatusModel>()
                .HasData(
                new StatusModel
                {
                    Id = new Guid("58297A8A-B5C3-4D1D-A587-07012190309C"),
                    Name = "Status 1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new StatusModel
                {
                    Id = new Guid("EE080251-D307-4F28-B733-77B6D1572A56"),
                    Name = "Status 2",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });

            modelBuilder.Entity<TransactionModel>()
                .HasData(
                new TransactionModel
                {
                    Id = new Guid("AD10BAE1-8C47-4610-8A64-2A61618E761D"),
                    Name = "Transaction 1",
                    Description = "Description 1",
                    IncomingStatusId = new Guid("58297A8A-B5C3-4D1D-A587-07012190309C"),
                    OutgoingStatusId = new Guid("EE080251-D307-4F28-B733-77B6D1572A56"),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new TransactionModel
                {
                    Id = new Guid("AD10BAE1-8C47-4610-8A64-2A61618E761C"),
                    Name = "Transaction 2",
                    Description = "Description 2",
                    IncomingStatusId = new Guid("EE080251-D307-4F28-B733-77B6D1572A56"),
                    OutgoingStatusId = new Guid("58297A8A-B5C3-4D1D-A587-07012190309C"),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });

            modelBuilder.Entity<UserModel>()
                .HasData(new UserModel
                {
                    Id = new Guid("E4BF0C15-A506-44F8-AB1C-6CD76CE93D9A"),
                    CreatedAt = DateTime.UtcNow,
                    Name = "Jan Kowalski"
                });

        }
    }
}
