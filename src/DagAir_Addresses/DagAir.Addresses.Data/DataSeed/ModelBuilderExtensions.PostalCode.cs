using DagAir.Addresses.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Addresses.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedPostalCodes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCode>()
                .HasData(
                    new PostalCode()
                    {
                        Id = 1,
                        Number = "04-265"
                    },
                    new PostalCode()
                    {
                        Id = 2,
                        Number = "25-685"
                    }
                );
        }
    }
}