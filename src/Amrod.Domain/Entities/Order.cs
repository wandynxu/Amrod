using Amrod.Domain.Common;
using Amrod.Domain.Common.Enums;
using Amrod.Infrastructure.Interceptors;

namespace Amrod.Domain.Entities;

public sealed class Order: BaseEntity, IAuditableEntity, ISoftDeleteEntity
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;  
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string CurrencyCode  { get; set; } 
    public decimal TotalAmount { get; set; }  
    public List<OrderLineItem> LineItems { get; set; } = [];
    public byte[] RowVersion { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}