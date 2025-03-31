﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PizzaDinner.Data;

#nullable disable

namespace PizzaDinner.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PizzaDinner.Models.Pizza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsVegetarian")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.ToTable("Pizza", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Clásica pizza con tomate y mozzarella",
                            IsVegetarian = true,
                            Name = "Margarita",
                            Price = 8.99m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Pizza con pepperoni y queso fundido",
                            IsVegetarian = false,
                            Name = "Pepperoni",
                            Price = 10.50m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
