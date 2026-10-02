using Amrod.Application.Features.Customers.Dtos;
using MediatR;

namespace Amrod.Application.Features.Customers.Queries.Search;

public sealed class SearchCustomersCommandHandler: IRequestHandler<SearchCustomerCommand, IReadOnlyList<CustomerDto>>
{
    public async Task<IReadOnlyList<CustomerDto>> Handle(SearchCustomerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}