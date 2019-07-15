﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VT.Data.TrackingHistory;

namespace VT.Data.Migrations.TrackingHistory
{
    [DbContext(typeof(TrackingHistoryContext))]
    partial class TrackingHistoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VT.Data.TrackingHistory.TrackingHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<decimal>("Lat");

                    b.Property<decimal>("Lon");

                    b.Property<Guid>("TrackingSessionId");

                    b.HasKey("Id");

                    b.HasIndex("TrackingSessionId");

                    b.ToTable("TrackingHistories");
                });

            modelBuilder.Entity("VT.Data.TrackingHistory.TrackingSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("TrackingRemark");

                    b.Property<Guid>("VehicleId");

                    b.HasKey("Id");

                    b.ToTable("TrackingSessions");
                });

            modelBuilder.Entity("VT.Data.TrackingHistory.TrackingHistory", b =>
                {
                    b.HasOne("VT.Data.TrackingHistory.TrackingSession", "TrackingSession")
                        .WithMany("TrackingHistories")
                        .HasForeignKey("TrackingSessionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
