﻿// <auto-generated />
using System;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250207193941_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "History"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Latest"
                        });
                });

            modelBuilder.Entity("Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "John Doe",
                            Description = "A comprehensive guide to C# programming.",
                            ISBN = "978-1234567890",
                            ListPrice = 49.990000000000002,
                            Price = 45.990000000000002,
                            Price100 = 35.990000000000002,
                            Price50 = 40.990000000000002,
                            Title = "C# Programming Guide"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Jane Smith",
                            Description = "Learn the essentials of ASP.NET Core.",
                            ISBN = "978-0987654321",
                            ListPrice = 59.990000000000002,
                            Price = 54.990000000000002,
                            Price100 = 45.990000000000002,
                            Price50 = 50.990000000000002,
                            Title = "ASP.NET Core Essentials"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Mark Johnson",
                            Description = "Master Entity Framework Core with real-world examples.",
                            ISBN = "978-1122334455",
                            ListPrice = 39.990000000000002,
                            Price = 35.990000000000002,
                            Price100 = 25.989999999999998,
                            Price50 = 30.989999999999998,
                            Title = "Entity Framework Core in Action"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Emily Davis",
                            Description = "A deep dive into LINQ with practical examples.",
                            ISBN = "978-2233445566",
                            ListPrice = 44.990000000000002,
                            Price = 40.990000000000002,
                            Price100 = 32.990000000000002,
                            Price50 = 36.990000000000002,
                            Title = "Mastering LINQ"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Michael Brown",
                            Description = "Learn how to build interactive web applications using Blazor WebAssembly.",
                            ISBN = "978-3344556677",
                            ListPrice = 69.989999999999995,
                            Price = 64.989999999999995,
                            Price100 = 54.990000000000002,
                            Price50 = 59.990000000000002,
                            Title = "Blazor WebAssembly Guide"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
