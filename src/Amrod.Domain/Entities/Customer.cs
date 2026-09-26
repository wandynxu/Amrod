using Amrod.Domain.Common;
using Amrod.Infrastructure.Interceptors;

namespace Amrod.Domain.Entities;

public sealed class Customer : BaseEntity, IAuditableEntity, ISoftDeleteEntity
{
    public string Name { get; set; } 
    public string Email { get; set; }
    public string CountryCode { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}