namespace Amrod.Application.Features.Customers.Dtos;

public record CustomerSearchDto
{
   public string[] Columns { get; init; } = ["name", "email", "countryCode"];
   public string? SearchTerm { get; init; }
   public int Page { get; init; }
   public int PageSize { get; init; }
}