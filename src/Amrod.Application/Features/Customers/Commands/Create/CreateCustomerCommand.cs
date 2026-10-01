using MediatR;

namespace Amrod.Application.Features.Customers.Commands.Create;

public record CreateCustomerCommand : IRequest<bool>
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string CountryCode { get; init; }
}