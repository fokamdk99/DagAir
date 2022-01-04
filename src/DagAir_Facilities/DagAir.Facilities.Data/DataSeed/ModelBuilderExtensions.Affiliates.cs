using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedAffiliates(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Affiliate>()
                .HasData(
                    new Affiliate()
                    {
                        Id = 1,
                        Name = "Faculty of Electronics and Information Technology",
                        AddressId = 1,
                        OrganizationId = 1L
                    },
                    new Affiliate()
                    {
                        Id = 2,
                        Name = "Faculty of Mathematics and Information Science",
                        AddressId = 2,
                        OrganizationId = 1L
                    },
                    new Affiliate()
                    {
                        Id = 3,
                        Name = "Collegium Of Economic Analysis",
                        AddressId = 2,
                        OrganizationId = 2L
                    }
                    );
        }
    }
}