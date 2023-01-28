﻿// <auto-generated />
using System;
using Lapshop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lapshop.Migrations
{
    [DbContext(typeof(LapshopDbContext))]
    [Migration("20230126184925_UpdateComparisonTable2")]
    partial class UpdateComparisonTable2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lapshop.Models.AccBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccBrands");
                });

            modelBuilder.Entity("Lapshop.Models.AccCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccCategories");
                });

            modelBuilder.Entity("Lapshop.Models.Accessory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SoldAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SellerId");

                    b.ToTable("Accessories");
                });

            modelBuilder.Entity("Lapshop.Models.AccessoryImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccId")
                        .HasColumnType("int");

                    b.Property<int?>("AccessoryId")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccessoryId");

                    b.ToTable("AccessoryImages");
                });

            modelBuilder.Entity("Lapshop.Models.AccessoryReviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("AccessoryReviews");
                });

            modelBuilder.Entity("Lapshop.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Lapshop.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Lapshop.Models.Comparison", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccessoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("CPU")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("DisplaySize")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("GPU")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("GPUBrand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HDD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasSSD")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LaptopId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Length")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProcessorBrand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessorType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RAMSize")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RAMType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("SSD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Comparisons");
                });

            modelBuilder.Entity("Lapshop.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Lapshop.Models.CustomerMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerMessages");
                });

            modelBuilder.Entity("Lapshop.Models.LapBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LapBrands");
                });

            modelBuilder.Entity("Lapshop.Models.LapCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LapCategories");
                });

            modelBuilder.Entity("Lapshop.Models.Laptop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<decimal>("CPU")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("DisplaySize")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("GPU")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("GPUBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HDD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasSSD")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Length")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProcessorBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessorType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RAMSize")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RAMType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("SSD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SellerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SoldAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Width")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SellerId");

                    b.ToTable("Laptops");
                });

            modelBuilder.Entity("Lapshop.Models.LaptopImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LapId")
                        .HasColumnType("int");

                    b.Property<int?>("LaptopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LaptopId");

                    b.ToTable("LaptopImages");
                });

            modelBuilder.Entity("Lapshop.Models.LaptopReviews", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LapId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("LaptopReviews");
                });

            modelBuilder.Entity("Lapshop.Models.Seller", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("Lapshop.Models.WhishListItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessoryId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("LaptopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccessoryId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LaptopId");

                    b.ToTable("WhishListItems");
                });

            modelBuilder.Entity("Lapshop.Models.Accessory", b =>
                {
                    b.HasOne("Lapshop.Models.AccBrand", "Brand")
                        .WithMany("Accessories")
                        .HasForeignKey("BrandId");

                    b.HasOne("Lapshop.Models.AccCategory", "Category")
                        .WithMany("Accessories")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Lapshop.Models.Seller", "Seller")
                        .WithMany("Accessories")
                        .HasForeignKey("SellerId");

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Lapshop.Models.AccessoryImages", b =>
                {
                    b.HasOne("Lapshop.Models.Accessory", "Accessory")
                        .WithMany("AccessoryImages")
                        .HasForeignKey("AccessoryId");

                    b.Navigation("Accessory");
                });

            modelBuilder.Entity("Lapshop.Models.CartItem", b =>
                {
                    b.HasOne("Lapshop.Models.Customer", "Customer")
                        .WithMany("CartItems")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Lapshop.Models.CustomerMessage", b =>
                {
                    b.HasOne("Lapshop.Models.Customer", "Cusotmer")
                        .WithMany("Messages")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cusotmer");
                });

            modelBuilder.Entity("Lapshop.Models.Laptop", b =>
                {
                    b.HasOne("Lapshop.Models.LapBrand", "Brand")
                        .WithMany("Laptops")
                        .HasForeignKey("BrandId");

                    b.HasOne("Lapshop.Models.LapCategory", "Category")
                        .WithMany("Laptops")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Lapshop.Models.Seller", "Seller")
                        .WithMany("Laptops")
                        .HasForeignKey("SellerId");

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Lapshop.Models.LaptopImages", b =>
                {
                    b.HasOne("Lapshop.Models.Laptop", "Laptop")
                        .WithMany("LaptopImages")
                        .HasForeignKey("LaptopId");

                    b.Navigation("Laptop");
                });

            modelBuilder.Entity("Lapshop.Models.WhishListItem", b =>
                {
                    b.HasOne("Lapshop.Models.Accessory", "Accessory")
                        .WithMany()
                        .HasForeignKey("AccessoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lapshop.Models.Customer", "Customer")
                        .WithMany("WhishListItems")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lapshop.Models.Laptop", "Laptop")
                        .WithMany()
                        .HasForeignKey("LaptopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accessory");

                    b.Navigation("Customer");

                    b.Navigation("Laptop");
                });

            modelBuilder.Entity("Lapshop.Models.AccBrand", b =>
                {
                    b.Navigation("Accessories");
                });

            modelBuilder.Entity("Lapshop.Models.AccCategory", b =>
                {
                    b.Navigation("Accessories");
                });

            modelBuilder.Entity("Lapshop.Models.Accessory", b =>
                {
                    b.Navigation("AccessoryImages");
                });

            modelBuilder.Entity("Lapshop.Models.Customer", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("Messages");

                    b.Navigation("WhishListItems");
                });

            modelBuilder.Entity("Lapshop.Models.LapBrand", b =>
                {
                    b.Navigation("Laptops");
                });

            modelBuilder.Entity("Lapshop.Models.LapCategory", b =>
                {
                    b.Navigation("Laptops");
                });

            modelBuilder.Entity("Lapshop.Models.Laptop", b =>
                {
                    b.Navigation("LaptopImages");
                });

            modelBuilder.Entity("Lapshop.Models.Seller", b =>
                {
                    b.Navigation("Accessories");

                    b.Navigation("Laptops");
                });
#pragma warning restore 612, 618
        }
    }
}
