using MediatR;

namespace Amrod.Application.Features.Customers.Commands.Create;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
{
    public Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}