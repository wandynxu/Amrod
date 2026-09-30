using Amrod.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amrod.Infrastructure.Persistence.Configurations;

public class OrderLineItemConfiguration : IEntityTypeConfiguration<OrderLineItem>
{
    public void Configure(EntityTypeBuilder<OrderLineItem> builder)
    {
        builder.ToTable(t => t.HasCheckConstraint("CK_Quantity_GreaterThanZero", "[Quantity] > 0"));
        builder.Property(oli => oli.ProductSku).HasMaxLength(20);
        builder.Property(oli => oli.UnitPrice).HasPrecision(18, 2);
    }
}