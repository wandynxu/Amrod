using Amrod.Application.Features.DTOs;
using MediatR;

namespace Amrod.Application.Features.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery : IRequest<CustomerDto?>
{
    public required Guid Id { get; init; }
}