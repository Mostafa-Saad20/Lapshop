using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Lapshop.Models;

namespace Lapshop.Models;

public partial class LapshopDbContext : DbContext
{
    public LapshopDbContext()
    {
    }

    public LapshopDbContext(DbContextOptions<LapshopDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LapshopDB;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Laptop> Laptops { get; set; }
    public DbSet<Accessory> Accessories { get; set; }
    public DbSet<LapCategory> LapCategories { get; set; }
    public DbSet<LapBrand> LapBrands { get; set; }
    public DbSet<AccCategory> AccCategories { get; set; }
    public DbSet<AccBrand> AccBrands { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<WhishListItem> WhishListItems { get; set; }
    public DbSet<LaptopReviews> LaptopReviews { get; set; }
    public DbSet<LaptopImages> LaptopImages { get; set; }
    public DbSet<AccessoryImages> AccessoryImages { get; set; }
	public DbSet<AccessoryReviews> AccessoryReviews { get; set; }
	public DbSet<CustomerMessage> CustomerMessages { get; set; }
	public DbSet<Comparison> Comparisons { get; set; }
	public DbSet<Order> Orders { get; set; }

}
