﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using QFSLINKS.Data;
using System;

namespace QFSLINKS.Migrations
{
    [DbContext(typeof(QfslinksContext))]
    [Migration("20180103221225_ChangeNames")]
    partial class ChangeNames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QFSLINKS.Models.SDR_QFS_Data", b =>
                {
                    b.Property<int>("TID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<string>("Description");

                    b.Property<string>("Detail");

                    b.Property<string>("Format");

                    b.Property<int>("GroupID");

                    b.Property<string>("Location");

                    b.Property<decimal?>("SortOrder");

                    b.Property<string>("Topic");

                    b.Property<int>("TopicID");

                    b.Property<string>("Type");

                    b.HasKey("TID");

                    b.ToTable("SDR_QFS_Data");
                });

            modelBuilder.Entity("QFSLINKS.Models.SDR_QFS_Datau", b =>
                {
                    b.Property<int>("TopicUserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Access");

                    b.Property<string>("AccessA");

                    b.Property<string>("Data");

                    b.Property<string>("Division");

                    b.Property<string>("Location");

                    b.Property<decimal>("SortOrder");

                    b.Property<int>("TopicID");

                    b.Property<string>("UserEmail");

                    b.Property<string>("UserInitials");

                    b.Property<string>("UserName");

                    b.Property<string>("UserPhone");

                    b.Property<string>("VimsAccess");

                    b.Property<int>("VimsDelegate");

                    b.Property<int>("VimsVisible");

                    b.HasKey("TopicUserID");

                    b.ToTable("SDR_QFS_DataU");
                });

            modelBuilder.Entity("QFSLINKS.Models.SDR_QFS_Division", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DivisionName");

                    b.HasKey("ID");

                    b.ToTable("SDR_QFS_Division");
                });
#pragma warning restore 612, 618
        }
    }
}