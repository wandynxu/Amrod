using Amrod.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Amrod.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.Name).HasMaxLength(100);
        builder.Property(c => c.Email).HasMaxLength(100);
        builder.Property(c => c.CountryCode).HasMaxLength(3).IsUnicode(false);
    }
}