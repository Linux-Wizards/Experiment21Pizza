namespace Experiment21.Data;

using Experiment21.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Seed initial data
        builder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Pepperoni Pizza", Price = 15.0M },
            new Product { Id = 2, Name = "Veggie Pizza", Price = 12.0M },
            new Product { Id = 3, Name = "Cheese Pizza", Price = 10.0M },
            new Product { Id = 4, Name = "Margherita Pizza", Price = 14.0M },
            new Product { Id = 5, Name = "BBQ Chicken Pizza", Price = 16.0M }
        );
    }
}
