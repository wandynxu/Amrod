using MediatR;

namespace Amrod.Application.Features.Customers.Commands.Search;

public sealed class SearchCustomersCommandHandler: IRequestHandler<SearchCustomerCommand>
{
    public async Task Handle(SearchCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}