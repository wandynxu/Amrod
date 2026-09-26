using Amrod.Domain.Common;
using Amrod.Domain.Interceptors;
using Amrod.Infrastructure.Interceptors;

namespace Amrod.Domain.Entities;

public sealed class Customer : BaseEntity, IAuditableEntity, ISoftDeleteEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string CountryCode { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}