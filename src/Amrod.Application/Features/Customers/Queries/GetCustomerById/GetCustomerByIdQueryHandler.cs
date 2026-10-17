using Amrod.Application.Features.DTOs;
using Amrod.Application.Mappings;
using Amrod.Domain.Entities;
using Amrod.Infrastructure.Persistence;
using MediatR;

namespace Amrod.Application.Features.Customers.Queries.GetCustomerById;

internal sealed class GetCustomerByIdQueryHandler(IRepository<Customer> repository): IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id);
        return customer?.ToCustomerDto();  
    }
}