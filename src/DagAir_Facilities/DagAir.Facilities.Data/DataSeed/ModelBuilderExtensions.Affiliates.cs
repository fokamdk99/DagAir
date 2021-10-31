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
                        OrganizationId = 1L
                    },
                    new Affiliate()
                    {
                        Id = 2,
                        Name = "Faculty of Mathematics and Information Science",
                        OrganizationId = 1L
                    },
                    new Affiliate()
                    {
                        Id = 3,
                        Name = "Collegium Of Economic Analysis",
                        OrganizationId = 2L
                    }
                    );
        }
    }
}