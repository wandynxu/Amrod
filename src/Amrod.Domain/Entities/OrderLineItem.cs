using Amrod.Domain.Common;
using Amrod.Infrastructure.Interceptors;

namespace Amrod.Domain.Entities;

public sealed class OrderLineItem: BaseEntity, IAuditableEntity,  ISoftDeleteEntity
{
    public Guid OrderId  { get; set; } 
    public string ProductSku  { get; set; }
    public int Quantity  { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}