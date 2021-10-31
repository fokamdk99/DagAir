using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Policies.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedExpectedRoomConditions(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpectedRoomConditions>()
                .HasData(
                    new ExpectedRoomConditions()
                    {
                        Id = 1,
                        Temperature = 20,
                        Illuminance = 100,
                        Humidity = 0.4f,
                        TemperatureMargin = 2,
                        IlluminanceMargin = 20,
                        HumidityMargin = 0.1f
                    },
                    new ExpectedRoomConditions()
                    {
                        Id = 2,
                        Temperature = 22,
                        Illuminance = 130,
                        Humidity = 0.5f,
                        TemperatureMargin = 3,
                        IlluminanceMargin = 30,
                        HumidityMargin = 0.1f
                    }
                );
        }
    }
}