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
                        EndDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours(2).Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        StartHour = 10,
                        EndHour = 12,
                        RepeatOn = "Monday, Thursday",
                        ExpectedConditionsId = 1L,
                        CategoryId = 1L,
                        RoomId = 1L,
                        CreatedBy = "dd8db40b-c091-4d12-a185-b2f71f369917",
                        SpansTwoDays = false
                    },
                    new RoomPolicy()
                    {
                        Id = 2,
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours(-3).Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        EndDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours(1).Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        StartHour = 12,
                        EndHour = 14,
                        RepeatOn = "Wednesday",
                        ExpectedConditionsId = 2L,
                        CategoryId = 2L,
                        RoomId = 1L,
                        CreatedBy = "dd8db40b-c091-4d12-a185-b2f71f369917",
                        SpansTwoDays = false
                    },
                    new RoomPolicy()
                    {
                        Id = 3,
                        StartDate = new DateTime(2021, 11, 5, 1, 1, 1),
                        EndDate = new DateTime(2022, 10, 5, 23, 59, 59),
                        StartHour = 22,
                        EndHour = 6,
                        RepeatOn = "",
                        ExpectedConditionsId = 2L,
                        CategoryId = 2L,
                        RoomId = 1L,
                        CreatedBy = "dd8db40b-c091-4d12-a185-b2f71f369917",
                        SpansTwoDays = true
                    },
                    new RoomPolicy()
                    {
                        Id = 4,
                        StartDate = new DateTime(1980, 11, 5, 1, 1, 1),
                        EndDate = new DateTime(9999, 12, 30, 23, 59, 59),
                        StartHour = 0,
                        EndHour = 23,
                        RepeatOn = "",
                        ExpectedConditionsId = 2L,
                        CategoryId = 1L,
                        RoomId = 0,
                        CreatedBy = "dd8db40b-c091-4d12-a185-b2f71f369917",
                        SpansTwoDays = false
                    }
                );
        }
    }
}