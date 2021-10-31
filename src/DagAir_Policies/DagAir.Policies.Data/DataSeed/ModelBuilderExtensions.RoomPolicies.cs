using System;
using DagAir.Policies.Data.AppEntities;
using Microsoft.EntityFrameworkCore;

namespace DagAir.Policies.Data.DataSeed
{
    public static partial class ModelBuilderExtensions
    {
        public static void SeedRoomPolicies(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomPolicy>()
                .HasData(
                    new RoomPolicy()
                    {
                        Id = 1,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours(2).Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        RepeatOn = "Monday, Thursday",
                        ExpectedConditionsId = 1L,
                        CategoryId = 1L,
                        RoomId = 1L
                    },
                    new RoomPolicy()
                    {
                        Id = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours(-3).Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours(1).Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        RepeatOn = "Wednesday",
                        ExpectedConditionsId = 2L,
                        CategoryId = 2L,
                        RoomId = 1L
                    },
                    new RoomPolicy()
                    {
                        Id = 3,
                        StartDate = new DateTime(2021, 11, 5, 1, 1, 1),
                        EndDate = new DateTime(2021, 10, 5, 23, 59, 59),
                        RepeatOn = "",
                        ExpectedConditionsId = 2L,
                        CategoryId = 2L,
                        RoomId = 1L
                    }
                );
        }
    }
}