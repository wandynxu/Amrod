using Amrod.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Amrod.Infrastructure.Persistence;

public class ApplicationDbContext: DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLineItem> OrderLineItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId);
        
        modelBuilder.Entity<OrderLineItem>()
             .HasOne(oli => oli.Order)
             .WithMany()
             .HasForeignKey(oli => oli.OrderId); 
         
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

