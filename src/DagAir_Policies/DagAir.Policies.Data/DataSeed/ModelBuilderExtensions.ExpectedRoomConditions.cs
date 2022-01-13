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
                        Humidity = (decimal) 0.4,
                        TemperatureMargin = 2,
                        IlluminanceMargin = 20,
                        HumidityMargin = (decimal) 0.1
                    },
                    new ExpectedRoomConditions()
                    {
                        Id = 2,
                        Temperature = 22,
                        Illuminance = 130,
                        Humidity = (decimal) 0.5,
                        TemperatureMargin = 3,
                        IlluminanceMargin = 30,
                        HumidityMargin = (decimal) 0.1
                    }
                );
        }
    }
}