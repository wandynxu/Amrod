using Amrod.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amrod.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.TotalAmount).HasPrecision(18, 2);
        builder.Property(o => o.CurrencyCode).HasMaxLength(3).IsUnicode(false);
        builder.Property(o => o.RowVersion).IsRowVersion();
        
        builder.HasIndex(o => new { o.CustomerId, o.Status, o.CreatedAt });
    }
}