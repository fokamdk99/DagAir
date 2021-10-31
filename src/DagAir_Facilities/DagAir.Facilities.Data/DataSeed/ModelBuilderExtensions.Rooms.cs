using System;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Facilities.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedRooms(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasData(
                    new Room()
                    {
                        Id = 1,
                        Number = "133",
                        Floor = 1,
                        AffiliateId = 1L
                    },
                    new Room()
                    {
                        Id = 2,
                        Number = "117",
                        Floor = 1,
                        AffiliateId = 2L
                    },
                    new Room()
                    {
                        Id = 3,
                        Number = "52",
                        Floor = 2,
                        AffiliateId = 3L
                    }
                    );
        }
    }
}