namespace Amrod.Application.Features.Customers.Dtos;

public record CustomerSearchDto
{
    public string? Search { get; init; }
    public int? Page { get; init; } 
    public int? PageSize { get; init; }
    public string? Sort { get; init; }
}