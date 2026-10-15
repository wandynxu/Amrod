namespace Amrod.Application.Features.DTOs;

public record SearchDto
{
    public string? Search { get; init; } 
    public int? Page { get; init; }
    public int? PageSize { get; init; }
    public string? Sort { get; init; }
    public string? OrderBy { get; init; }
}