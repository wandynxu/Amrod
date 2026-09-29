using MediatR;

namespace Amrod.Application.Features.Customers.Commands.Create;

public class CreateCustomerCommand  : IRequest
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string CountryCode { get; private set; }

    public CreateCustomerCommand(string name, string email, string countryCode)
    {
        Name = name;
        Email = email;
        CountryCode = countryCode;
    }
}