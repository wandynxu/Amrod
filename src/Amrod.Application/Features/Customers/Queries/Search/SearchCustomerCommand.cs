using Amrod.Application.Features.Customers.Dtos;
using MediatR;

namespace Amrod.Application.Features.Customers.Queries.Search;

public record SearchCustomerCommand : IRequest<IReadOnlyList<CustomerDto>>
{
    public string? Search { get; init; }
    public int? Page { get; init; }
    public int? PageSize { get; init; }
    public string? Sort { get; init; }
    public string[] Columns { get; } = ["Name", "Email", "CountryCode"];
}