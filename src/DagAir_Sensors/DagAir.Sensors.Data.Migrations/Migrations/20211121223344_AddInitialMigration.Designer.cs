// <auto-generated />
using System;
using DagAir.Sensors.Data.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DagAir.Sensors.Data.Migrations.Migrations
{
    [DbContext(typeof(DagAirSensorAppContext))]
    [Migration("20211121223344_AddInitialMigration")]
    partial class AddInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("DagAir.Sensors")
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("DagAir.Sensors.Data.AppEntities.Producer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("AddressId")
                        .HasColumnType("bigint")
                        .HasColumnName("address_id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateOfEstablishment")
                        .HasColumnType("datetime")
                        .HasColumnName("date_of_establishment");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_producers");

                    b.ToTable("producers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AddressId = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfEstablishment = new DateTime(2021, 11, 19, 23, 33, 43, 842, DateTimeKind.Local).AddTicks(5997),
                            Name = "Saturn"
                        },
                        new
                        {
                            Id = 2L,
                            AddressId = 2L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfEstablishment = new DateTime(2021, 11, 14, 23, 33, 43, 844, DateTimeKind.Local).AddTicks(8028),
                            Name = "Euro agd"
                        });
                });

            modelBuilder.Entity("DagAir.Sensors.Data.AppEntities.Sensor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("AffiliateId")
                        .HasColumnType("bigint")
                        .HasColumnName("affiliate_id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("LastDataSentDate")
                        .HasColumnType("datetime")
                        .HasColumnName("last_data_sent_date");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.Property<long>("RoomId")
                        .HasColumnType("bigint")
                        .HasColumnName("room_id");

                    b.Property<long>("SensorModelId")
                        .HasColumnType("bigint")
                        .HasColumnName("sensor_model_id");

                    b.HasKey("Id")
                        .HasName("pk_sensors");

                    b.HasIndex("SensorModelId")
                        .HasDatabaseName("ix_sensors_sensor_model_id");

                    b.ToTable("sensors");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AffiliateId = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastDataSentDate = new DateTime(2021, 11, 21, 18, 33, 43, 846, DateTimeKind.Local).AddTicks(2687),
                            RoomId = 1L,
                            SensorModelId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            AffiliateId = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastDataSentDate = new DateTime(2021, 11, 21, 20, 33, 43, 846, DateTimeKind.Local).AddTicks(3388),
                            RoomId = 1L,
                            SensorModelId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            AffiliateId = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastDataSentDate = new DateTime(2021, 11, 21, 22, 33, 43, 846, DateTimeKind.Local).AddTicks(3397),
                            RoomId = 1L,
                            SensorModelId = 3L
                        });
                });

            modelBuilder.Entity("DagAir.Sensors.Data.AppEntities.SensorModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("created")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("name");

                    b.Property<long>("ProducerId")
                        .HasColumnType("bigint")
                        .HasColumnName("producer_id");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_sensor_models");

                    b.HasIndex("ProducerId")
                        .HasDatabaseName("ix_sensor_models_producer_id");

                    b.ToTable("sensor_models");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "illuminati",
                            ProducerId = 1L,
                            Version = "v1"
                        },
                        new
                        {
                            Id = 2L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "humidati",
                            ProducerId = 1L,
                            Version = "v1"
                        },
                        new
                        {
                            Id = 3L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "tempurati",
                            ProducerId = 2L,
                            Version = "v1"
                        });
                });

            modelBuilder.Entity("DagAir.Sensors.Data.AppEntities.Sensor", b =>
                {
                    b.HasOne("DagAir.Sensors.Data.AppEntities.SensorModel", "SensorModel")
                        .WithMany("Sensors")
                        .HasForeignKey("SensorModelId")
                        .HasConstraintName("fk_sensors_sensor_models_sensor_model_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SensorModel");
                });

            modelBuilder.Entity("DagAir.Sensors.Data.AppEntities.SensorModel", b =>
                {
                    b.HasOne("DagAir.Sensors.Data.AppEntities.Producer", "Producer")
                        .WithMany("SensorModels")
                        .HasForeignKey("ProducerId")
                        .HasConstraintName("fk_sensor_models_producers_producer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("DagAir.Sensors.Data.AppEntities.Producer", b =>
                {
                    b.Navigation("SensorModels");
                });

            modelBuilder.Entity("DagAir.Sensors.Data.AppEntities.SensorModel", b =>
                {
                    b.Navigation("Sensors");
                });
#pragma warning restore 612, 618
        }
    }
}
