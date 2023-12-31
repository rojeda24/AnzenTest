﻿// <auto-generated />
using System;
using Anzen.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Anzen.Migrations
{
    [DbContext(typeof(SatoriContext))]
    [Migration("20231231042855_StatusFK")]
    partial class StatusFK
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("Anzen.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Anzen.Models.Submission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("EffectiveDate")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("ExpirationDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Premium")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Sic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UwName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Submission");
                });

            modelBuilder.Entity("Anzen.Models.Submission", b =>
                {
                    b.HasOne("Anzen.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
