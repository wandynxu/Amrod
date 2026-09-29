using MediatR;

namespace Amrod.Application.Features.Customers.Commands.Create;

public record CreateCustomerCommand(string Name, string Email, string CountryCode) : IRequest<bool>
{
    
}