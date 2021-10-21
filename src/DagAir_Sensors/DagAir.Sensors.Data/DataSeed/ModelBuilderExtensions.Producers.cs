using System;
using DagAir.QueryNode.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Sensors.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedProducers(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producer>()
                .HasData(
                    new Producer()
                    {
                        Id = 1,
                        Name = "Saturn",
                        AddressId = 1,
                        DateOfEstablishment = DateTime.Now.AddDays(-2)
                    },
                    new Producer()
                    {
                        Id = 2,
                        Name = "Euro agd",
                        AddressId = 2,
                        DateOfEstablishment = DateTime.Now.AddDays(-7)
                    });
        }
    }
}