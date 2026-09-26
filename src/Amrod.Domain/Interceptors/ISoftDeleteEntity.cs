namespace Amrod.Infrastructure.Interceptors;

public interface ISoftDeleteEntity
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; } 
}