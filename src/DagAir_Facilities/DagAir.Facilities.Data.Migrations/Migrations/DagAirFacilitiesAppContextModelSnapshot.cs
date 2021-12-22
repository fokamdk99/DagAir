﻿// <auto-generated />
using System;
using DagAir.Facilities.Data.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DagAir.Facilities.Data.Migrations.Migrations
{
    [DbContext(typeof(DagAirFacilitiesAppContext))]
    partial class DagAirFacilitiesAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("DagAir.Facilities")
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("DagAir.Facilities.Data.AppEntitities.Affiliate", b =>
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

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("name");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint")
                        .HasColumnName("organization_id");

                    b.HasKey("Id")
                        .HasName("pk_affiliates");

                    b.HasIndex("OrganizationId")
                        .HasDatabaseName("ix_affiliates_organization_id");

                    b.ToTable("affiliates");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AddressId = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Faculty of Electronics and Information Technology",
                            OrganizationId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            AddressId = 2L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Faculty of Mathematics and Information Science",
                            OrganizationId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            AddressId = 2L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Collegium Of Economic Analysis",
                            OrganizationId = 2L
                        });
                });

            modelBuilder.Entity("DagAir.Facilities.Data.AppEntitities.Organization", b =>
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

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_organizations");

                    b.ToTable("organizations");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AddressId = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Warsaw University Of Technology"
                        },
                        new
                        {
                            Id = 2L,
                            AddressId = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Warsaw School Of Economics"
                        });
                });

            modelBuilder.Entity("DagAir.Facilities.Data.AppEntitities.Room", b =>
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

                    b.Property<int>("Floor")
                        .HasColumnType("int")
                        .HasColumnName("floor");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("datetime")
                        .HasColumnName("modified");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("number");

                    b.Property<byte[]>("UniqueRoomId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)")
                        .HasColumnName("unique_room_id")
                        .HasDefaultValueSql("(UUID_TO_BIN(UUID()))");

                    b.HasKey("Id")
                        .HasName("pk_rooms");

                    b.HasIndex("AffiliateId")
                        .HasDatabaseName("ix_rooms_affiliate_id");

                    b.ToTable("rooms");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AffiliateId = 1L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Floor = 1,
                            Number = "133",
                            UniqueRoomId = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                        },
                        new
                        {
                            Id = 2L,
                            AffiliateId = 2L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Floor = 1,
                            Number = "117",
                            UniqueRoomId = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                        },
                        new
                        {
                            Id = 3L,
                            AffiliateId = 3L,
                            Created = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Floor = 2,
                            Number = "52",
                            UniqueRoomId = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
                        });
                });

            modelBuilder.Entity("DagAir.Facilities.Data.AppEntitities.Affiliate", b =>
                {
                    b.HasOne("DagAir.Facilities.Data.AppEntitities.Organization", "Organization")
                        .WithMany("Affiliates")
                        .HasForeignKey("OrganizationId")
                        .HasConstraintName("fk_affiliates_organizations_organization_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("DagAir.Facilities.Data.AppEntitities.Room", b =>
                {
                    b.HasOne("DagAir.Facilities.Data.AppEntitities.Affiliate", "Affiliate")
                        .WithMany("Rooms")
                        .HasForeignKey("AffiliateId")
                        .HasConstraintName("fk_rooms_affiliates_affiliate_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Affiliate");
                });

            modelBuilder.Entity("DagAir.Facilities.Data.AppEntitities.Affiliate", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("DagAir.Facilities.Data.AppEntitities.Organization", b =>
                {
                    b.Navigation("Affiliates");
                });
#pragma warning restore 612, 618
        }
    }
}
