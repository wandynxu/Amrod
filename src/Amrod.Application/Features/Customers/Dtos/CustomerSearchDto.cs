namespace Amrod.Application.Features.Customers.Dtos;

public record CustomerSearchDto
{
   public string Search { get; init; } = string.Empty;
   public int Page { get; init; } = 1;
   public int PageSize { get; init; } = 100;
   public string Sort { get; init; } = "asc";
   public string[] Columns { get; init; } = ["name", "email", "countryCode"];
}