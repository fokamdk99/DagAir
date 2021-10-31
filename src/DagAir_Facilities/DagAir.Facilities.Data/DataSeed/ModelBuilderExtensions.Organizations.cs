using System;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedOrganizations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasData(
                    new Organization()
                    {
                        Id = 1,
                        Name = "Warsaw University Of Technology",
                        AddressId = 1
                    },
                    new Organization()
                    {
                        Id = 2,
                        Name = "Warsaw School Of Economics",
                        AddressId = 1
                    }
                    );
        }
    }
}