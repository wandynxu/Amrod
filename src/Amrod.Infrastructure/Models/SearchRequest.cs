namespace Amrod.Infrastructure.Models;

public record SearchRequest
{
    public string Search { get; init; } = string.Empty;
    public int Page { get; init; } 
    public int PageSize { get; init; }
    public string Sort { get; init; } = string.Empty;
    public string[] Columns { get; init; } = [];
}