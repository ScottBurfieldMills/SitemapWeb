﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SitemapWeb.Data;

namespace SitemapWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SitemapWeb.Entities.SitemapSubmission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("SitemapSubmissionStatusId");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.HasKey("Id");

                    b.HasIndex("SitemapSubmissionStatusId");

                    b.ToTable("SitemapSubmissions");
                });

            modelBuilder.Entity("SitemapWeb.Entities.SitemapSubmissionStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SitemapSubmissionStatuses");
                });

            modelBuilder.Entity("SitemapWeb.Entities.SitemapSubmission", b =>
                {
                    b.HasOne("SitemapWeb.Entities.SitemapSubmissionStatus", "SitemapSubmissionStatus")
                        .WithMany()
                        .HasForeignKey("SitemapSubmissionStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
