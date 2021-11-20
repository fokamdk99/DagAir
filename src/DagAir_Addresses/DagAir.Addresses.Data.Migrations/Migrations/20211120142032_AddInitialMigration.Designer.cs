﻿// <auto-generated />
using System;
using DagAir.Addresses.Data.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DagAir.Addresses.Data.Migrations.Migrations
{
    [DbContext(typeof(DagAirAddressesAppContext))]
    [Migration("20211120142032_AddInitialMigration")]
    partial class AddInitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("DagAir.Addresses")
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("DagAir.Addresses.Data.AppEntities.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("CityId")
                        .HasColumnType("bigint")
                        .HasColumnName("city_id");

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint")
                        .HasColumnName("country_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("created")
                        .HasDefaultValueSql("(CURRENT_DATE)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.Property<long>("PostalCodeId")
                        .HasColumnType("bigint")
                        .HasColumnName("postal_code_id");

                    b.HasKey("Id")
                        .HasName("pk_addresses");

                    b.HasIndex("CityId")
                        .HasDatabaseName("ix_addresses_city_id");

                    b.HasIndex("CountryId")
                        .HasDatabaseName("ix_addresses_country_id");

                    b.HasIndex("PostalCodeId")
                        .HasDatabaseName("ix_addresses_postal_code_id");

                    b.ToTable("addresses");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CityId = 1L,
                            CountryId = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PostalCodeId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            CityId = 2L,
                            CountryId = 2L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PostalCodeId = 2L
                        });
                });

            modelBuilder.Entity("DagAir.Addresses.Data.AppEntities.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("created")
                        .HasDefaultValueSql("(CURRENT_DATE)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_cities");

                    b.ToTable("cities");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Stockholm"
                        },
                        new
                        {
                            Id = 2L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Reykjavik"
                        });
                });

            modelBuilder.Entity("DagAir.Addresses.Data.AppEntities.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("created")
                        .HasDefaultValueSql("(CURRENT_DATE)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_countries");

                    b.ToTable("countries");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Sweden"
                        },
                        new
                        {
                            Id = 2L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Iceland"
                        });
                });

            modelBuilder.Entity("DagAir.Addresses.Data.AppEntities.PostalCode", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime")
                        .HasColumnName("created")
                        .HasDefaultValueSql("(CURRENT_DATE)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("number");

                    b.HasKey("Id")
                        .HasName("pk_postal_codes");

                    b.ToTable("postal_codes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Number = "04-265"
                        },
                        new
                        {
                            Id = 2L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Number = "25-685"
                        });
                });

            modelBuilder.Entity("DagAir.Addresses.Data.AppEntities.Address", b =>
                {
                    b.HasOne("DagAir.Addresses.Data.AppEntities.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityId")
                        .HasConstraintName("fk_addresses_cities_city_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DagAir.Addresses.Data.AppEntities.Country", "Country")
                        .WithMany("Addresses")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("fk_addresses_countries_country_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DagAir.Addresses.Data.AppEntities.PostalCode", "PostalCode")
                        .WithMany("Addresses")
                        .HasForeignKey("PostalCodeId")
                        .HasConstraintName("fk_addresses_postal_codes_postal_code_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("PostalCode");
                });

            modelBuilder.Entity("DagAir.Addresses.Data.AppEntities.City", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("DagAir.Addresses.Data.AppEntities.Country", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("DagAir.Addresses.Data.AppEntities.PostalCode", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
