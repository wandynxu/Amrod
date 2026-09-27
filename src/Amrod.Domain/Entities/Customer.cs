using Amrod.Domain.Common;
using Amrod.Domain.Interceptors;
using Amrod.Infrastructure.Interceptors;

namespace Amrod.Domain.Entities;

public sealed class Customer : BaseEntity, IAuditableEntity, ISoftDeleteEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}