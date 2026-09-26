using Amrod.Domain.Common;
using Amrod.Domain.Interceptors;
using Amrod.Infrastructure.Interceptors;

namespace Amrod.Domain.Entities;

public sealed class OrderLineItem: BaseEntity, IAuditableEntity,  ISoftDeleteEntity
{
    public Guid OrderId  { get; set; } 
    public Order Order  { get; set; }  = null!;  
    public required string ProductSku  { get; set; }
    public int Quantity  { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}