using Amrod.Application.Features.DTOs;
using MediatR;

namespace Amrod.Application.Features.Customers.Commands.Search;

public record SearchCustomersCommand : IRequest<IReadOnlyList<CustomerDto>>
{
    public string? Search { get; init; } 
    public int? Page { get; init; }
    public int? PageSize { get; init; }
    public string? Sort { get; init; }
    public string? OrderBy { get; init; }
    public string[] Columns { get; init; } =  ["Name", "Email", "CountryCode"];
}