using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Policies.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedRoomPolicyConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomPolicyCategory>()
                .HasData(
                    new RoomPolicyCategory()
                    {
                        Id = 1,
                        Name = "Default",
                        CategoryNumber = 0
                    },
                    new RoomPolicyCategory()
                    {
                        Id = 2,
                        Name = "Organizational",
                        CategoryNumber = 1
                    },
                    new RoomPolicyCategory()
                    {
                        Id = 3,
                        Name = "Departmental",
                        CategoryNumber = 2
                    },
                    new RoomPolicyCategory()
                    {
                        Id = 4,
                        Name = "Custom",
                        CategoryNumber = 3
                    }
                );
        }
    }
}