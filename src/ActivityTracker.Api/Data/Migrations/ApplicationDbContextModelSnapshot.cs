﻿// <auto-generated />
using System;
using ActivityTracker.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ActivityTracker.Api.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("ActivityTracker.Api.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("IssueId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(550)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("Activities", (string)null);
                });

            modelBuilder.Entity("ActivityTracker.Api.Models.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CloseDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(550)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Issues", (string)null);
                });

            modelBuilder.Entity("ActivityTracker.Api.Models.Activity", b =>
                {
                    b.HasOne("ActivityTracker.Api.Models.Issue", "Issue")
                        .WithMany("Activities")
                        .HasForeignKey("IssueId");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("ActivityTracker.Api.Models.Issue", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}