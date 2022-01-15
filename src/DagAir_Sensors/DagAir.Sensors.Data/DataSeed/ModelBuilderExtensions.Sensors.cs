using System;
using DagAir.Sensors.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Sensors.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedSensors(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sensor>()
                .HasData(
                    new Sensor()
                    {
                        Id = 1,
                        SensorName = "com.gonzalo789.esp32",
                        LastDataSentDate = DateTime.Now.AddHours(-5),
                        RoomId = 1,
                        AffiliateId = 1,
                        SensorModelId = 1 
                    },
                    new Sensor()
                    {
                        Id = 2,
                        SensorName = "wemos_stas1",
                        LastDataSentDate = DateTime.Now.AddHours(-3),
                        RoomId = 1,
                        AffiliateId = 1,
                        SensorModelId = 2 
                    },
                    new Sensor()
                    {
                        Id = 3,
                        SensorName = "wemos_stas2",
                        LastDataSentDate = DateTime.Now.AddHours(-1),
                        RoomId = 2,
                        AffiliateId = 1,
                        SensorModelId = 3 
                    });
        }
    }
}