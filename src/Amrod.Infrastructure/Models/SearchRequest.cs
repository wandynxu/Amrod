namespace Amrod.Infrastructure.Models;

public record SearchRequest
{
    public string[] SearchTerms { get; init; } = [];
    public int Page { get; init; } 
    public int PageSize { get; init; }
    public string Sort { get; init; } = string.Empty;
    public string[] Columns { get; init; } = [];
}