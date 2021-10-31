using DagAir.Sensors.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Sensors.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedSensorModels(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensorModel>()
                .HasData(
                    new SensorModel()
                    {
                        Id = 1,
                        Name = "illuminati",
                        Version = "v1",
                        ProducerId = 1L
                    },
                    new SensorModel()
                    {
                        Id = 2,
                        Name = "humidati",
                        Version = "v1",
                        ProducerId = 1L
                    },
                    new SensorModel()
                    {
                        Id = 3,
                        Name = "tempurati",
                        Version = "v1",
                        ProducerId = 2L
                    });
        }
    }
}