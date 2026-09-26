namespace Amrod.Domain.Interceptors;

public interface IAuditableEntity
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}