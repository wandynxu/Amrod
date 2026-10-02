using Amrod.Domain.Entities;
using Amrod.Infrastructure.Persistence;
using MediatR;

namespace Amrod.Application.Features.Customers.Commands.Create;

public sealed class CreateCustomerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerCommand, bool>
{
    public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken ct)
    {
        
        var customer = new Customer
        {
            Name = request.Name,
            Email = request.Email,
            CountryCode = request.CountryCode
        };
        
        var customerExist = await unitOfWork.GetRepository<Customer>().GetByFilterAsync(c => c.Email == customer.Email) is not null;
        if (customerExist) return true;
        
         await unitOfWork.GetRepository<Customer>().Create(customer);
         await unitOfWork.CommitAsync();
         return false;
    }
}