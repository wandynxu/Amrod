using Amrod.Domain.Entities;
using Amrod.Infrastructure.Persistence;
using MediatR;

namespace Amrod.Application.Features.Customers.Commands.Create;

internal sealed class CreateCustomerCommandHandler(IRepository<Customer> repository,ApplicationDbContext context) : IRequestHandler<CreateCustomerCommand, bool>
{
    public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken ct)
    {
        
        var customer = new Customer
        {
            Name = request.Name,
            Email = request.Email,
            CountryCode = request.CountryCode
        };
        
        var customerExist = await repository.GetByFilterAsync(c => c.Email == customer.Email) is not null;
        if (customerExist) return true;
        
         await repository.Create(customer);
         await context.SaveChangesAsync(ct);
         return false;
    }
}