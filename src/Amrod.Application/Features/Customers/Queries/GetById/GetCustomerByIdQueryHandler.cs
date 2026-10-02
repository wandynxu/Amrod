using Amrod.Application.Features.Customers.Dtos;
using Amrod.Application.Mappings;
using Amrod.Domain.Entities;
using Amrod.Infrastructure.Persistence;
using MediatR;

namespace Amrod.Application.Features.Customers.Queries.GetById;

public sealed class GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork): IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.GetRepository<Customer>().GetByFilterAsync(c => c.Id == request.Id);
        return customer?.ToCustomerDto();
    }
}