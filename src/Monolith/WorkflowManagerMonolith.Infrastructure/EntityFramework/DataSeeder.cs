using Microsoft.EntityFrameworkCore;
using System;
using WorkflowManagerMonolith.Core.Domain;

namespace WorkflowManagerMonolith.Infrastructure.EntityFramework
{
    public class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            
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

            modelBuilder.Entity<ApplicationEntity>()
                .HasData(new ApplicationEntity(new Guid("5660AF80-C763-4EFC-86FC-B3986FEA59D4")));

            modelBuilder.Entity<TransactionItem>()
                .HasData(
                    TransactionItem.Create(
                         new TransactionEntity(
                            new Guid("AD10BAE1-8C47-4610-8A64-2A61618E761D"),
                            "Transaction 1",
                            "Description 1",
                            new Guid("58297A8A-B5C3-4D1D-A587-07012190309C"),
                            new Guid("EE080251-D307-4F28-B733-77B6D1572A56")
                        ),
                        new Guid("E4BF0C15-A506-44F8-AB1C-6CD76CE93D9A"),
                        new Guid("5660AF80-C763-4EFC-86FC-B3986FEA59D4")
                    )
                );

        }
    }
}
