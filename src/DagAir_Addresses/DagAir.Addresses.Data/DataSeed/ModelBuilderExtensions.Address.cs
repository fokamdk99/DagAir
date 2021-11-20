using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Addresses.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedAddresses(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasData(
                    new Address()
                    {
                        Id = 1,
                        CountryId = 1,
                        CityId = 1,
                        PostalCodeId = 1
                    },
                    new Address()
                    {
                        Id = 2,
                        CountryId = 2,
                        CityId = 2,
                        PostalCodeId = 2
                    }
                );
        }
    }
}