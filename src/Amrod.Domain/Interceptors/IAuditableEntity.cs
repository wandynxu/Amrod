namespace Amrod.Infrastructure.Interceptors;

public interface IAuditableEntity
{
    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }
}