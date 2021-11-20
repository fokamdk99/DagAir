using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Addresses.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedCountries(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .HasData(
                    new Country()
                    {
                        Id = 1,
                        Name = "Sweden"
                    },
                    new Country()
                    {
                        Id = 2,
                        Name = "Iceland"
                    }
                );
        }
    }
}