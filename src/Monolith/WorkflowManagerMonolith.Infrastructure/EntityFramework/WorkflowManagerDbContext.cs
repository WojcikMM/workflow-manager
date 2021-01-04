using Microsoft.EntityFrameworkCore;
using System;
using WorkflowManagerMonolith.Core.Domain;
// using WorkflowManagerMonolith.Infrastructure.EntityFramework.Models;

namespace WorkflowManagerMonolith.Infrastructure.EntityFramework
{
    public class WorkflowManagerDbContext : DbContext
    {
        public WorkflowManagerDbContext(DbContextOptions<WorkflowManagerDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<ApplicationEntity> Applications { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<StatusEntity> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primmary Keys
            modelBuilder.Entity<StatusEntity>().HasKey(status => status.Id);
            modelBuilder.Entity<TransactionEntity>().HasKey(transaction => transaction.Id);
            modelBuilder.Entity<UserEntity>().HasKey(user => user.Id);
            modelBuilder.Entity<ApplicationEntity>().HasKey(applicaiton => applicaiton.Id);

            // Relations

            // -- Transaction Model

            modelBuilder.Entity<TransactionEntity>()
                .HasOne(transaction => transaction.OutgoingStatus)
                .WithMany(status => status.IncomingTransactions)
                .HasForeignKey(transaction => transaction.OutgoingStatusId);

            modelBuilder.Entity<TransactionEntity>()
                .HasOne(transaction => transaction.IncomingStatus)
                .WithMany(status => status.AvailableTransactions)
                .HasForeignKey(transaction => transaction.IncomingStatusId);

            // -- Status Model

            modelBuilder.Entity<StatusEntity>()
                .HasMany(status => status.AvailableTransactions)
                .WithOne(transaction => transaction.IncomingStatus)
                .HasForeignKey(transaction => transaction.IncomingStatusId)
                .IsRequired();

            modelBuilder.Entity<StatusEntity>()
                .HasMany(status => status.IncomingTransactions)
                .WithOne(transaction => transaction.OutgoingStatus)
                .HasForeignKey(transaction => transaction.OutgoingStatusId)
                .IsRequired();

            //modelBuilder.Entity<StatusEntity>()
            //    .HasMany(status => status.ApplicationsWithStatus)
            //    .WithOne(application => application.Status)
            //    .HasForeignKey(application => application.StatusId)
            //    .IsRequired(false);


            // -- Transaction Item Model
            modelBuilder.Entity<TransactionItem>()
                .HasKey(transactionItem => transactionItem.Id);

            modelBuilder.Entity<TransactionItem>()
                .HasOne(transactionItem => transactionItem.Application)
                .WithMany(application => application.TransactionItems)
                .HasForeignKey(transactionItem => transactionItem.ApplicationId)
                .IsRequired();

            modelBuilder.Entity<TransactionItem>()
                .HasOne(transactionItem => transactionItem.Transaction)
                .WithMany(transaction => transaction.TransactionItems)
                .HasForeignKey(transactionItem => transactionItem.TransactionId)
                .IsRequired();

            modelBuilder.Entity<TransactionItem>()
                .HasOne(transactionItem => transactionItem.User)
                .WithMany(user => user.TransactionItems)
                .HasForeignKey(transactionitems => transactionitems.UserId)
                .IsRequired();

            // -- User Model
            modelBuilder.Entity<UserEntity>()
                .HasMany(user => user.AssignedApplications)
                .WithOne(application => application.User)
                .HasForeignKey(application => application.AssignedUserId);


            // Seed Data

            modelBuilder.Entity<StatusEntity>()
                .HasData(
                new StatusEntity(new Guid("58297A8A-B5C3-4D1D-A587-07012190309C"), "Status 1"),
                new StatusEntity(new Guid("EE080251-D307-4F28-B733-77B6D1572A56"), "Status 2"),
                new StatusEntity(new Guid("EE080251-D307-4F28-B733-77B6D1572A51"), "Status 3"));

            modelBuilder.Entity<TransactionEntity>()
                .HasData(
                new TransactionEntity(
                    new Guid("AD10BAE1-8C47-4610-8A64-2A61618E761D"),
                    "Transaction 1",
                    "Description 1",
                    new Guid("58297A8A-B5C3-4D1D-A587-07012190309C"),
                    new Guid("EE080251-D307-4F28-B733-77B6D1572A56")
                    ),
                new TransactionEntity(
                    new Guid("0DF10806-95B7-46C2-B777-01645AE94474"),
                    "Transaction 2",
                    "Description 2",
                    new Guid("58297A8A-B5C3-4D1D-A587-07012190309C"),
                    new Guid("EE080251-D307-4F28-B733-77B6D1572A56")
                ),
                new TransactionEntity(
                    new Guid("AD10BAE1-8C47-4610-8A64-2A61618E761E"),
                    "Transaction 3",
                    "Description 3",
                    new Guid("58297A8A-B5C3-4D1D-A587-07012190309C"),
                    new Guid("EE080251-D307-4F28-B733-77B6D1572A51")
                    )
                );


            modelBuilder.Entity<UserEntity>()
                .HasData(new UserEntity(new Guid("E4BF0C15-A506-44F8-AB1C-6CD76CE93D9A"), "Jan Kowalski"));

        }
    }
}
