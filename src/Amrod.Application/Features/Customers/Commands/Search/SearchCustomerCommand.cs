namespace Amrod.Application.Features.Customers.Commands.Search;

public record SearchCustomerCommand
{
     public string? Name { get; init; }
     public string? Email { get; init; }
     public int Page { get; init; }
     public int PageSize { get; init; }
};