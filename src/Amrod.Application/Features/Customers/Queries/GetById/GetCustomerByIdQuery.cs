using Amrod.Application.Features.Customers.Dtos;
using MediatR;

namespace Amrod.Application.Features.Customers.Queries.GetById;

public record GetCustomerByIdQuery : IRequest<CustomerDto?>
{
    public required Guid Id { get; init; }
}