using System.ComponentModel.DataAnnotations;

namespace Amrod.Domain.Common;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}