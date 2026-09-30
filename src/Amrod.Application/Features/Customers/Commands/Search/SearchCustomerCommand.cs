using Amrod.Application.Features.Customers.Dtos;
using MediatR;

namespace Amrod.Application.Features.Customers.Commands.Search;

public record SearchCustomerCommand(string[] Columns, string? SearchTerm, int Page, int PageSize) : IRequest<IReadOnlyList<CustomerDto>>
{
}