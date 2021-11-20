using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Addresses.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedCities(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                    new City()
                    {
                        Id = 1,
                        Name = "Stockholm"
                    },
                    new City()
                    {
                        Id = 2,
                        Name = "Reykjavik"
                    }
                );
        }
    }
}